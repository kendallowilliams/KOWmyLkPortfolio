import BaseClass from "../../assets/models/base-class";
import HtmlControls from '../../assets/controls/html-controls';
import LoadingModal from '../../assets/modals/loading-modal';

export default class ScaffoldDbContextProfiles extends BaseClass {

    constructor() {
        super();
        this.initialize();
    }

    private initialize(): void {
        const $selectedProfile = $('[data-profile-id].active'),
            $firstProfile = $('[data-profile-id]').first();

        this.initializeControls();

        if ($selectedProfile.length > 0)  $selectedProfile.trigger('click');
        else $firstProfile.trigger('click');
    }

    private initializeControls(): void {
        const modals = HtmlControls.Modals();

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

    private loadProfile(id): void {
        const containers = HtmlControls.Containers();

        LoadingModal.showLoading();
        $(containers.ScaffoldDbContextsProfileContainer).load('/ScaffoldDbContextProfiles/ScaffoldDbContextProfile?id=' + id, () => {
            const buttons = HtmlControls.Buttons();

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
            LoadingModal.hideLoading();
        });
    }

    private addTable(): void {
        const inputs = HtmlControls.Inputs(),
            containers = HtmlControls.Containers(),
            templates = HtmlControls.Templates(),
            $table = $(inputs.TxtScaffoldDbContextTable),
            table = $table.val() as string,
            $list = $('#lstTables'),
            $newTable = $(templates.ScaffoldDbContextTableTemplate).clone(true);

        if (table) {
            $list.append('<option selected="selected" data-saved="false">' + table + '</option>');
            $newTable.removeClass('d-none').removeAttr('data-template').attr('data-saved', 'false');
            $newTable.find('.input-group-text').text(table);
            $(containers.ScaffoldDbContextsTableContainer).append($newTable);
            $table.val('');
        }
    }

    private deleteTable(row): void {
        const $row = $(row),
            $list = $('#lstTables'),
            table = $row.find('.input-group-text').text();

        $row.remove();
        $list.find('option:contains("' + table + '")').remove();
    }

    private resetProfile(): void {
        const form = HtmlControls.Forms().ScaffoldDbContextForm;

        form.reset();
        $(form).find('[data-saved="false"]').remove();
    }

    private addProfile(name): void {
        LoadingModal.showLoading();
        $.post('/ScaffoldDbContextProfiles/AddScaffoldDbContextProfile', { name: name }, (data) => {
            window.location.href = 'ScaffoldDbContextProfiles/Index?id=' + data;
        });
    }

    private deleteProfile(id): void {
        if (window.confirm('Please confirm that you want to delete this profile...')) {
            LoadingModal.showLoading();
            $.post('/ScaffoldDbContextProfiles/DeleteScaffoldDbContextProfile', { id: id }, (data) => {
                window.location.href = 'ScaffoldDbContextProfiles/Index';
            });
        }
    }

    private saveProfile(): void {
        const containers = HtmlControls.Containers(),
            form = HtmlControls.Forms().ScaffoldDbContextForm,
            formData = new FormData(form),
            success = (data, status, xhr) => {
                $(containers.ScaffoldDbContextsProfileContainer).html(data);
                LoadingModal.hideLoading();
            },
            error = (xhr, status, error) => {
                LoadingModal.hideLoading();
                alert(status);
            };

        LoadingModal.showLoading();
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

    private run(id): void {
        LoadingModal.showLoading();
        $.post('/ScaffoldDbContextProfiles/Process', { id: id }, function () {
            LoadingModal.hideLoading();
        });
    }
}