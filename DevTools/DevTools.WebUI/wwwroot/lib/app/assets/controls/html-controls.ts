export default {
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
        ScaffoldDbContextForm: document.querySelector('[data-form="ScaffoldDbContextProfile"]') as HTMLFormElement
    }),
    Inputs: () => ({
        TxtScaffoldDbContextTable: document.getElementById('txt-scaffold-db-context-table')
    }),
    Templates: () => ({
        ScaffoldDbContextTableTemplate: document.querySelector('[data-template="ScaffoldDbContextTableTemplate"]')
    })
}