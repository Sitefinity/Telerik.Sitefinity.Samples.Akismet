Type.registerNamespace("AkismetModule.UI");

AkismetModule.UI.AkismetBasicSettingsView = function (element) {
    AkismetModule.UI.AkismetBasicSettingsView.initializeBase(this, [element]);
    this._ajaxResponseDelegate = null;
    this._ajaxResponseErrorDelegate = null;
    this._apiKeyField = null;
    this._protectCommentsField = null;
    this._protectForumsField = null;
    this._messageControl = null;
    this._saveButton = null;
    this._saveButtonClickDelegate = null;
    this._saveSettingsResponseDelegate = null;
    this._saveSettingsErrorDelegate = null;
}

AkismetModule.UI.AkismetBasicSettingsView.prototype = {
    initialize: function () {
        AkismetModule.UI.AkismetBasicSettingsView.callBaseMethod(this, 'initialize');

        this._ajaxResponseDelegate = Function.createDelegate(this, this._handleServiceResponse);
        this._ajaxResponseErrorDelegate = Function.createDelegate(this, this._handleServiceError);
        this._saveButtonClickDelegate = Function.createDelegate(this, this._handleSaveButtonClick);
        this._saveSettingsResponseDelegate = Function.createDelegate(this, this._handleSaveSettingsResponse);
        this._saveSettingsErrorDelegate = Function.createDelegate(this, this._handleSaveSettingsError);

        jQuery(this.get_saveButton()).on('click', this._saveButtonClickDelegate);

        jQuery.ajax({
            url: "/sitefinity/services/akismet/akismetservice.svc/",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            //context: this,
            success: this._ajaxResponseDelegate,
            error: this._ajaxResponseErrorDelegate
        });
    },
    dispose: function () {
        AkismetModule.UI.AkismetBasicSettingsView.callBaseMethod(this, 'dispose');

        if (this._ajaxResponseDelegate)
            delete this._ajaxResponseDelegate;
        if (this._ajaxResponseErrorDelegate)
            delete this._ajaxResponseErrorDelegate;
        if (this._saveButtonClickDelegate)
            delete this._saveButtonClickDelegate;
        if (this._saveSettingsResponseDelegate)
            delete this._saveSettingsResponseDelegate;
        if (this._saveSettingsErrorDelegate)
            delete this._saveSettingsErrorDelegate;
    },

    _handleSaveButtonClick: function (sender, args) {
        var settings = {
            //"viewModel": {
                "ApiKey": this.get_apiKeyField().get_value(),
                "ProtectComments": this.get_protectCommentsField().get_value(),
                "ProtectForums": this.get_protectForumsField().get_value()
            //}
        };

        jQuery.ajax({
            url: "/sitefinity/services/akismet/akismetservice.svc/",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify(settings),
            //processData: false,
            success: this._saveSettingsResponseDelegate,
            error: this._saveSettingsErrorDelegate
        });
    },

    _handleServiceResponse: function (data, jqXHR, textStatus) {
        this.get_apiKeyField().set_value(data.ApiKey);
        this.get_protectCommentsField().set_value(data.ProtectComments);
        this.get_protectForumsField().set_value(data.ProtectForums);
    },

    _handleServiceError: function (jqXHR, textStatus, error) {
        this.get_messageControl().showNegativeMessage("A problem occurred while loading settings: " + error);
    },

    _handleSaveSettingsResponse: function (data, jqXHR, textStatus) {
        this.get_messageControl().showPositiveMessage("Settings saved successfully.");
    },

    _handleSaveSettingsError: function (jqXHR, textStatus, error) {
        this.get_messageControl().showNegativeMessage("A problem occurred while saving settings: " + error);
    },

    get_protectCommentsField: function () {
        return this._protectCommentsField;
    },

    set_protectCommentsField: function (value) {
        if (value) {
            this._protectCommentsField = value;
        }
    },

    get_protectForumsField: function () {
        return this._protectForumsField;
    },

    set_protectForumsField: function (value) {
        if (value) {
            this._protectForumsField = value;
        }
    },

    get_apiKeyField: function () {
        return this._apiKeyField;
    },

    set_apiKeyField: function (value) {
        if (value) {
            this._apiKeyField = value;
        }
    },

    get_messageControl: function () {
        return this._messageControl;
    },

    set_messageControl: function (value) {
        if (value) {
            this._messageControl = value;
        }
    },

    get_saveButton: function () {
        return this._saveButton;
    },

    set_saveButton: function (value) {
        this._saveButton = value;
    }
}

AkismetModule.UI.AkismetBasicSettingsView.registerClass('AkismetModule.UI.AkismetBasicSettingsView', Sys.UI.Control);