using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forums.Web.UI;
using Telerik.Sitefinity.Modules.Blogs.Web.UI;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Samples.Common;
using Telerik.Sitefinity.Services;

namespace SitefinityWebApp
{
    public class Global : System.Web.HttpApplication
    {
        private const string SamplesThemeName = "SamplesTheme";
        private const string SamplesThemePath = "~/App_Data/Sitefinity/WebsiteTemplates/Samples/App_Themes/Samples";

        private const string SamplesTemplateId = "03b770d0-c143-4aa5-a3c0-2891bfb31f14";
        private const string SamplesTemplateName = "SamplesMasterPage";
        private const string SamplesTemplatePath = "~/App_Data/Sitefinity/WebsiteTemplates/Samples/App_Master/Samples.master";

        private const string ForumGroupId = "CBD71839-C47F-4937-8AFF-4860579481F5";
        private const string ForumGroupTitle = "Akismet Sample Forums";
        private const string ForumId = "CBD71839-C47F-4937-8AFF-4860579481F6";
        private const string ForumTitle = "Akismet Forum";
        private const string ForumThreadId = "CBD71839-C47F-4937-8AFF-4860579481F7";
        private const string ForumPostId = "CBD71839-C47F-4937-8AFF-4860579481F8";

        private const string AkismetBlogId = "DE26A02F-3785-46CD-AF8B-1A86D7091439";
        private const string AkismetBlogPostId = "DE26A02F-3785-46CD-AF8B-1A86D7091440";

        private const string AkismetBlogTitle = "Akismet Blog";
        private const string AkismetBlogDescription = "A blog to test Akismet spam protection for comments";

        private const string HomePageId = "E1F546F9-041F-42D9-89B6-B879803C2B46";
        private const string ForumsPageId = "E1F546F9-041F-42D9-89B6-B879803C2B47";
        private const string BlogPageId = "E1F546F9-041F-42D9-89B6-B879803C2B48";

        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Initialized += this.Bootstrapper_Initialized;
        }

        private void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "RegisterRoutes")
            {
                SampleUtilities.RegisterModule<AkismetModule.AkismetModule>("Akismet", "Showcases Forums Akismet integration");
            }

            if ((Bootstrapper.IsDataInitialized) && (e.CommandName == "Bootstrapped"))
            {
                SystemManager.RunWithElevatedPrivilegeDelegate worker = new SystemManager.RunWithElevatedPrivilegeDelegate(this.CreateSample);
                SystemManager.RunWithElevatedPrivilege(worker);
            }
        }

        private void CreateSample(object[] args)
        {
            SampleUtilities.RegisterTheme(SamplesThemeName, SamplesThemePath);
            SampleUtilities.RegisterTemplate(new Guid(SamplesTemplateId), SamplesTemplateName, SamplesTemplateName, SamplesTemplatePath, SamplesThemeName);

            this.CreateForums();
            this.CreateBlogs();

            var result = SampleUtilities.CreatePage(new Guid(HomePageId), "Home", true);

            if (result)
            {
                SampleUtilities.SetTemplateToPage(new Guid(HomePageId), new Guid(SamplesTemplateId));

                ContentBlockBase cb = new ContentBlockBase();
                cb.Html = "<h1>Akismet Integration</h1><p>This sample demonstrates how to integrate Akismet with Sitefinity using the EventHub mechanism. You can submit new Forum posts or Blog comments, which will be checked through Akismet for spam.</p>";

                SampleUtilities.AddControlToPage(new Guid(HomePageId), cb, "Content", "Content Block");
            }

            var result2 = SampleUtilities.CreatePage(new Guid(ForumsPageId), "Forum");
            if (result2)
            {
                SampleUtilities.SetTemplateToPage(new Guid(ForumsPageId), new Guid(SamplesTemplateId));

                ForumsView forums = new ForumsView();
                SampleUtilities.AddControlToPage(new Guid(ForumsPageId), forums, "Content", "Forums");
            }

            var result3 = SampleUtilities.CreatePage(new Guid(BlogPageId), "Blog");
            if (result3)
            {
                SampleUtilities.SetTemplateToPage(new Guid(BlogPageId), new Guid(SamplesTemplateId));

                BlogPostView blogs = new BlogPostView();
                SampleUtilities.AddControlToPage(new Guid(BlogPageId), blogs, "Content", "Blog Posts");
            }
        }

        private void CreateForums()
        {
            var success = SampleUtilities.CreateForumGroup(new Guid(ForumGroupId), ForumGroupTitle, string.Empty);

            SampleUtilities.CreateForum(new Guid(ForumId), new Guid(ForumGroupId), ForumTitle, string.Empty);
            SampleUtilities.CreateForumThreadFromPost(new Guid(ForumId), new Guid(ForumThreadId), new Guid(ForumPostId), "You can post in this thread to test the Akismet sample", "This is the first post.");
        }

        private void CreateBlogs()
        {
            var result = SampleUtilities.CreateBlog(new Guid(AkismetBlogId), AkismetBlogTitle, AkismetBlogDescription);
            SampleUtilities.CreateBlogPost(
                new Guid(AkismetBlogId),
                new Guid(AkismetBlogPostId),
                "Akismet Integration in Sitefinity",
                "You can submit comments to this blog post to test the Akismet integration. They will be checked through Akismet to confirm they're not spam",
                "Telerik",
                "A blog post to test comment submission");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}