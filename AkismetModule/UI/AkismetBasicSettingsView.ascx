<%@ Control Language="C#" %>



<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.FieldControls" TagPrefix="sfFields" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sfFields" %>

<sf:ClientLabelManager id="clientLabelManager" runat="server">
    <labels>
        <sf:ClientLabel ClassId="Labels" Key="SectionSaved" runat="server" />
    </labels>
</sf:ClientLabelManager>

<sfFields:FormManager ID="formManager" runat="server" />

<div class="sfTwitterSettingsWrp">
    <sf:Message 
        runat="server" 
        ID="message" 
        ElementTag="div"  
        CssClass="sfMessage sfDialogMessage"
        RemoveAfter="5000"  
        FadeDuration="10" />

    <div id="loadingView" runat="server" style="display: none;" class="sfLoadingFormBtns sfButtonArea">
        <sf:SfImage ID="loadingImage1" runat="server" AlternateText="<%$Resources:Labels, SavingImgAlt %>" />
    </div>

    <div class="sfSettingsSection">
        <h2 id="sfAddTwitterAppTitle">
            <asp:Literal ID="Literal1" runat="server" Text="<%$Resources: AkismetResources, AkismetBasicSettingsViewTitle %>" /></h2>
        <asp:PlaceHolder ID="placeHolder" runat="server" />
        
    </div>

    <sfFields:FieldControlsBinder ID="fieldsBinder" runat="server" TargetId="fieldsBinder"
        DataKeyNames="Id" ServiceUrl="~/Sitefinity/Services/Configuration/ConfigSectionItems.svc/comments/">
    </sfFields:FieldControlsBinder>

    <div class="sfSettingsSection">
        <sfFields:TextField ID="akismetApiKey" runat="server" DataFieldName="ApiKey" DisplayMode="Write" Title="Akismet API key"></sfFields:TextField>
    </div>

    <ul class="sfCheckListBox sfSettingsSection">
        <li>
            <sfFields:ChoiceField ID="protectForums" runat="server" DataFieldName="ProtectForums" DisplayMode="Write" RenderChoicesAs="SingleCheckBox">
                <Choices>
                    <sfFields:ChoiceItem Text="<%$Resources:AkismetResources, ProtectForumsCaption %>" />
                </Choices>
            </sfFields:ChoiceField>
        </li>

        <li>
            <sfFields:ChoiceField ID="protectComments" runat="server" DataFieldName="ProtectComments" DisplayMode="Write" RenderChoicesAs="SingleCheckBox">
                <Choices>
                    <sfFields:ChoiceItem Text="<%$Resources:AkismetResources, ProtectCommentsCaption %>" />
                </Choices>
            </sfFields:ChoiceField>
        </li>
    </ul>
    <div class="sfButtonArea" id="buttonsArea" runat="server">
        <asp:HyperLink ID="btnSave" runat="server" class="sfLinkBtn sfSave">
            <strong class="sfLinkBtnIn">
                <asp:Literal runat="server" Text='<%$ Resources:Labels, SaveChangesLabel %>' />
            </strong>
        </asp:HyperLink>
    </div>

    <script>
        
    </script>
</div>