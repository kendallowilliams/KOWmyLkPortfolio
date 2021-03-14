define("assets/models/base-class", ["require", "exports", "jquery"], function (require, exports, jQuery) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class BaseClass {
        constructor() {
            this.$ = jQuery;
        }
    }
    exports.default = BaseClass;
});
define("assets/controls/html-controls", ["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        Modals: () => ({
            LoadingModal: document.getElementById('loading-modal'),
            AddScaffoldDbContextProfileModal: document.getElementById('add-scaffold-db-context-profile-modal')
        }),
        Buttons: () => ({
            BtnSaveScaffoldDbContextProfile: document.getElementById('btn-save-scaffold-dbcontext-profile'),
            BtnUndoScaffoldDbContextProfile: document.getElementById('btn-undo-scaffold-dbcontext-profile'),
            BtnDeleteScaffoldDbContextProfile: document.getElementById('btn-delete-scaffold-dbcontext-profile'),
            BtnRunScaffoldDbContextProfile: document.getElementById('btn-run-scaffold-dbcontext-profile'),
            BtnAddScaffoldDbContextTable: document.getElementById('btn-add-scaffold-db-context-table')
        }),
        Containers: () => ({
            ScaffoldDbContextsProfileContainer: document.querySelector('[data-container="ScaffoldDbContextProfilesContainer"]'),
            ScaffoldDbContextsTableContainer: document.querySelector('[data-container="ScaffoldDbContextTableContainer"]')
        }),
        Forms: () => ({
            ScaffoldDbContextForm: document.querySelector('[data-form="ScaffoldDbContextProfile"]')
        }),
        Inputs: () => ({
            TxtScaffoldDbContextTable: document.getElementById('txt-scaffold-db-context-table')
        }),
        Templates: () => ({
            ScaffoldDbContextTableTemplate: document.querySelector('[data-template="ScaffoldDbContextTableTemplate"]')
        })
    };
});
define("assets/modals/loading-modal", ["require", "exports", "assets/controls/html-controls"], function (require, exports, html_controls_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        showLoading: function () {
            const $modal = $(html_controls_1.default.Modals().LoadingModal);
            $modal.modal('show');
        },
        hideLoading: function () {
            const $modal = $(html_controls_1.default.Modals().LoadingModal);
            $modal.modal('hide');
        }
    };
});
define("devtools/scaffold-db-context-profiles/scaffold-db-context-profiles", ["require", "exports", "assets/models/base-class", "assets/controls/html-controls", "assets/modals/loading-modal"], function (require, exports, base_class_1, html_controls_2, loading_modal_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class ScaffoldDbContextProfiles extends base_class_1.default {
        constructor() {
            super();
            this.initialize();
        }
        initialize() {
            const $selectedProfile = $('[data-profile-id].active'), $firstProfile = $('[data-profile-id]').first();
            this.initializeControls();
            if ($selectedProfile.length > 0)
                $selectedProfile.trigger('click');
            else
                $firstProfile.trigger('click');
        }
        initializeControls() {
            const modals = html_controls_2.default.Modals();
            $(modals.AddScaffoldDbContextProfileModal).find('[data-button="Add"]').on('click', () => {
                const profileName = $(modals.AddScaffoldDbContextProfileModal).find('input').val();
                $(modals.AddScaffoldDbContextProfileModal).modal('hide');
                this.addProfile(profileName);
            });
            $(modals.AddScaffoldDbContextProfileModal).on('show.bs.modal', function (e) {
                const $modal = $(e.currentTarget);
                $modal.find('input').val('');
            });
            $('[data-profile-id]').on('click', (e) => {
                const id = $(e.currentTarget).attr('data-profile-id');
                this.loadProfile(id);
            });
        }
        loadProfile(id) {
            const containers = html_controls_2.default.Containers();
            loading_modal_1.default.showLoading();
            $(containers.ScaffoldDbContextsProfileContainer).load('/ScaffoldDbContextProfiles/ScaffoldDbContextProfile?id=' + id, () => {
                const buttons = html_controls_2.default.Buttons();
                $('[data-toggle="tooltip"]').tooltip('dispose');
                $('[data-profile-id]').removeClass('active');
                $('[data-profile-id="' + id + '"]').addClass('active');
                $('[data-toggle="tooltip"]').tooltip();
                $(buttons.BtnSaveScaffoldDbContextProfile).on('click', e => {
                    this.saveProfile();
                });
                $(buttons.BtnUndoScaffoldDbContextProfile).on('click', e => {
                    this.resetProfile();
                });
                $(buttons.BtnDeleteScaffoldDbContextProfile).on('click', e => {
                    const id = $(e.currentTarget).attr('data-item-id');
                    this.deleteProfile(id);
                });
                $(buttons.BtnRunScaffoldDbContextProfile).on('click', e => {
                    const id = $(e.currentTarget).attr('data-item-id');
                    this.run(id);
                });
                $(buttons.BtnAddScaffoldDbContextTable).on('click', () => {
                    this.addTable();
                });
                $('[data-btn-delete="ScaffoldDbContextTable"]').on('click', e => {
                    this.deleteTable(e.currentTarget.parentNode.parentNode);
                });
                loading_modal_1.default.hideLoading();
            });
        }
        addTable() {
            const inputs = html_controls_2.default.Inputs(), containers = html_controls_2.default.Containers(), templates = html_controls_2.default.Templates(), $table = $(inputs.TxtScaffoldDbContextTable), table = $table.val(), $list = $('#lstTables'), $newTable = $(templates.ScaffoldDbContextTableTemplate).clone();
            if (table) {
                $list.append('<option selected="selected" data-saved="false">' + table + '</option>');
                $newTable.removeClass('d-none').removeAttr('data-template').attr('data-saved', 'false').find('.input-group-text').text(table);
                $(containers.ScaffoldDbContextsTableContainer).append($newTable);
                $table.val('');
            }
        }
        deleteTable(row) {
            const $row = $(row), $list = $('#lstTables'), table = $row.find('.input-group-text').text();
            $row.remove();
            $list.find('option:contains("' + table + '")').remove();
        }
        resetProfile() {
            const form = html_controls_2.default.Forms().ScaffoldDbContextForm;
            form.reset();
            $(form).find('[data-saved="false"]').remove();
        }
        addProfile(name) {
            loading_modal_1.default.showLoading();
            $.post('/ScaffoldDbContextProfiles/AddScaffoldDbContextProfile', { name: name }, (data) => {
                window.location.href = 'ScaffoldDbContextProfiles/Index?id=' + data;
            });
        }
        deleteProfile(id) {
            if (window.confirm('Please confirm that you want to delete this profile...')) {
                loading_modal_1.default.showLoading();
                $.post('/ScaffoldDbContextProfiles/DeleteScaffoldDbContextProfile', { id: id }, (data) => {
                    window.location.href = 'ScaffoldDbContextProfiles/Index';
                });
            }
        }
        saveProfile() {
            const containers = html_controls_2.default.Containers(), form = html_controls_2.default.Forms().ScaffoldDbContextForm, formData = new FormData(form), success = (data, status, xhr) => {
                $(containers.ScaffoldDbContextsProfileContainer).html(data);
                loading_modal_1.default.hideLoading();
            }, error = (xhr, status, error) => {
                loading_modal_1.default.hideLoading();
                alert(status);
            };
            loading_modal_1.default.showLoading();
            $.ajax({
                url: '/ScaffoldDbContextProfiles/UpdateScaffoldDbContextProfile',
                data: formData,
                processData: false,
                contentType: false,
                type: 'POST',
                success: success,
                error: error
            });
        }
        run(id) {
            loading_modal_1.default.showLoading();
            $.post('/ScaffoldDbContextProfiles/Process', { id: id }, function () {
                loading_modal_1.default.hideLoading();
            });
        }
    }
    exports.default = ScaffoldDbContextProfiles;
});
define("devtools/settings/settings", ["require", "exports", "assets/models/base-class"], function (require, exports, base_class_2) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Settings extends base_class_2.default {
        constructor() {
            super();
            this.initialize();
        }
        initialize() {
        }
    }
    exports.default = Settings;
});
define("devtools/devtools", ["require", "exports", "assets/models/base-class", "devtools/scaffold-db-context-profiles/scaffold-db-context-profiles", "devtools/settings/settings"], function (require, exports, base_class_3, scaffold_db_context_profiles_1, settings_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class DevTools extends base_class_3.default {
        constructor() {
            super();
            this.initialize();
        }
        initialize() {
            this.scaffoldDbContextProfiles = new scaffold_db_context_profiles_1.default();
            this.settings = new settings_1.default();
        }
    }
    exports.default = DevTools;
});
define("app", ["require", "exports", "devtools/devtools"], function (require, exports, devtools_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class App {
        constructor() {
            this.initialize();
            this.devTools = new devtools_1.default();
        }
        initialize() {
        }
    }
    exports.default = App;
});
