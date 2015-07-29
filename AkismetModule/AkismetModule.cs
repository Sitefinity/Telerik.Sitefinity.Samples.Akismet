using System;
using System.Linq;
using System.Web;
using AkismetModule.Model;
using AkismetModule.Services;
using AkismetModule.UI;
using Joel.Net;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Forums;
using Telerik.Sitefinity.Forums.Events;
using Telerik.Sitefinity.Forums.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Blogs;
using Telerik.Sitefinity.Modules.GenericContent.Events;
using Telerik.Sitefinity.Services;

namespace AkismetModule
{
    public class AkismetModule : ModuleBase
    {
        public const string ModuleName = "Akismet";

        public static readonly string AkismetResourceClass = "AkismetResources";

        public override void Install(SiteInitializer initializer)
        {
            // nothing to install. This module only subscribes to events raised by Forums and Comments in the Initialize method
        }

        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);
            App.WorkWith().Module(settings.Name).Initialize()
                .Configuration<AkismetModuleConfig>()
                .WebService<AkismetService>("Sitefinity/Services/Akismet/AkismetService.svc/")
                .Localization<AkismetResources>();

            EventHub.Subscribe<IForumPostCreatingEvent>(evt => this.ValidateCreatingForumPost(evt.Item));
            EventHub.Subscribe<IForumPostUpdatingEvent>(evt => this.ValidateUpdatingForumPost(evt.Item, evt.Origin, evt.UserId));

            EventHub.Subscribe<ICommentCreatingEvent>(evt => this.ValidateCreatingComment(evt.DataItem));
            EventHub.Subscribe<ICommentUpdatingEvent>(evt => this.ValidateUpdatingComment(evt.DataItem));

            SystemManager.RegisterBasicSettings<AkismetBasicSettingsView>("AkismetSettings", "AkismetSettingsTitle", "AkismetResources");
        }

        private void ValidateCreatingForumPost(ForumPost post)
        {
            var catchSpamInForums = Config.Get<AkismetModuleConfig>().ProtectForums;
            if (catchSpamInForums)
            {
                Akismet akismetApiClient = new Akismet(Config.Get<AkismetModuleConfig>().ApiKey, "http://www.sitefinity.com", "SitefinityAkismetModule");
                if (!akismetApiClient.VerifyKey())
                {
                    return;
                }

                var newAkismetData = new AkismetData()
                {
                    AkismetDataId = Guid.NewGuid(),
                    ContentItemId = post.Id,
                    ItemType = typeof(ForumPost).FullName,
                    UserIp = HttpContext.Current.Request.UserHostAddress,
                    UserAgent = HttpContext.Current.Request.UserAgent,
                    Referrer = HttpContext.Current.Request.UrlReferrer.OriginalString
                };

                var newForumPost = new AkismetComment()
                {
                    Blog = "http://www.sitefinity.com",
                    CommentContent = post.Content,
                    CommentType = "comment",
                    Referrer = newAkismetData.Referrer,
                    UserAgent = newAkismetData.UserAgent,
                    UserIp = newAkismetData.UserIp,
                };
                var isSpam = akismetApiClient.CommentCheck(newForumPost);
                post.IsMarkedSpam = isSpam;
                if (isSpam)
                    post.IsPublished = false;

                var akismetDbContext = new AkismetEntityContext();
                akismetDbContext.AkismetDataList.Add(newAkismetData);
                akismetDbContext.SaveChanges();
            }
        }

        private void ValidateUpdatingForumPost(ForumPost post, string origin, Guid userId)
        {
            var catchSpamInForums = Config.Get<AkismetModuleConfig>().ProtectForums;
            if (catchSpamInForums)
            {
                var forumsMan = ForumsManager.GetManager(string.Empty, "DummyTransaction");

                var existingPost = forumsMan.GetPost(post.Id);
                if (existingPost != null && existingPost.IsMarkedSpam != post.IsMarkedSpam)
                {
                    Akismet akismetApiClient = new Akismet(Config.Get<AkismetModuleConfig>().ApiKey, "http://www.sitefinity.com", "SitefinityAkismetModule");
                    if (!akismetApiClient.VerifyKey())
                    {
                        return;
                    }

                    var akismetDbContext = new AkismetEntityContext();
                    var existingAkismetData = akismetDbContext.AkismetDataList.SingleOrDefault(a => a.ContentItemId == post.Id);
                    if (existingAkismetData != null)
                    {
                        var updatedForumPost = new AkismetComment()
                        {
                            Blog = "http://www.sitefinity.com",
                            CommentContent = post.Content,
                            CommentType = "comment",
                            Referrer = existingAkismetData.Referrer,
                            UserAgent = existingAkismetData.UserAgent,
                            UserIp = existingAkismetData.UserIp,
                        };

                        if (post.IsMarkedSpam)
                        {
                            // the item has been marked as spam
                            akismetApiClient.SubmitSpam(updatedForumPost);
                        }
                        else
                        {
                            // the item has been marked as ham
                            akismetApiClient.SubmitHam(updatedForumPost);
                        }
                    }
                }
            }
        }

        private void ValidateCreatingComment(Comment comment)
        {
            var catchSpamInComments = Config.Get<AkismetModuleConfig>().ProtectComments;
            if (catchSpamInComments)
            {
                Akismet akismetApiClient = new Akismet(Config.Get<AkismetModuleConfig>().ApiKey, "http://www.sitefinity.com", "SitefinityAkismetModule");
                if (!akismetApiClient.VerifyKey())
                {
                    return;
                }

                var newAkismetData = new AkismetData()
                {
                    AkismetDataId = Guid.NewGuid(),
                    ContentItemId = comment.Id,
                    ItemType = typeof(Comment).FullName,
                    UserIp = HttpContext.Current.Request.UserHostAddress,
                    UserAgent = HttpContext.Current.Request.UserAgent,
                    Referrer = HttpContext.Current.Request.UrlReferrer.OriginalString,
                };

                var newComment = new AkismetComment()
                {
                    Blog = "http://www.sitefinity.com",
                    CommentContent = comment.Content,
                    CommentType = "comment",
                    Referrer = newAkismetData.Referrer,
                    UserAgent = newAkismetData.UserAgent,
                    UserIp = newAkismetData.UserIp,
                    CommentAuthor = comment.AuthorName,
                    CommentAuthorEmail = comment.Email,
                    CommentAuthorUrl = comment.Website
                };
                var isSpam = akismetApiClient.CommentCheck(newComment);

                if (isSpam)
                {
                    comment.CommentStatus = CommentStatus.Spam;
                }

                var akismetDbContext = new AkismetEntityContext();
                akismetDbContext.AkismetDataList.Add(newAkismetData);
                akismetDbContext.SaveChanges();
            }
        }

        private void ValidateUpdatingComment(Comment comment)
        {
            var catchSpamInComments = Config.Get<AkismetModuleConfig>().ProtectComments;
            if (catchSpamInComments)
            {
                var blogsMan = BlogsManager.GetManager(string.Empty, "DummyTransaction");
                var existingComment = SystemManager.GetCommentsService().GetComment(comment.Id.ToString());

                if (existingComment != null && existingComment.Status != comment.Status.ToString())
                {
                    Akismet akismetApiClient = new Akismet(Config.Get<AkismetModuleConfig>().ApiKey, "http://www.sitefinity.com", "SitefinityAkismetModule");
                    if (!akismetApiClient.VerifyKey())
                    {
                        return;
                    }

                    var akismetDbContext = new AkismetEntityContext();
                    var existingAkismetData = akismetDbContext.AkismetDataList.SingleOrDefault(a => a.ContentItemId == comment.Id);
                    if (existingAkismetData != null)
                    {
                        var updatedComment = new AkismetComment()
                        {
                            Blog = "http://www.sitefinity.com",
                            CommentContent = comment.Content,
                            CommentType = "comment",
                            Referrer = existingAkismetData.Referrer,
                            UserAgent = existingAkismetData.UserAgent,
                            UserIp = existingAkismetData.UserIp,
                            CommentAuthor = comment.AuthorName,
                            CommentAuthorEmail = comment.Email,
                            CommentAuthorUrl = comment.Website
                        };

                        if (comment.CommentStatus.ToString() == Telerik.Sitefinity.Services.Comments.StatusConstants.Spam)
                        {
                            // the item has been marked as spam
                            akismetApiClient.SubmitSpam(updatedComment);
                        }
                        else
                        {
                            // the item has been marked as ham
                            akismetApiClient.SubmitHam(updatedComment);
                        }
                    }
                }
            }
        }

        public override Type[] Managers
        {
            get
            {
                return AkismetModule.managerTypes;
            }
        }

        public override Guid LandingPageId
        {
            get
            {
                return SiteInitializer.DashboardPageNodeId;
            }
        }

        public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
        {
            // Nothing to upgrade yet
        }

        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<AkismetModuleConfig>();
        }

        private static readonly Type[] managerTypes = null;
    }
}
