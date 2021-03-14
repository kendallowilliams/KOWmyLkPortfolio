import HtmlControls from '../controls/html-controls';

export default {
    showLoading: function (): void {
        const $modal = $(HtmlControls.Modals().LoadingModal);

        $modal.modal('show');
    },
    hideLoading: function (): void {
        const $modal = $(HtmlControls.Modals().LoadingModal);

        $modal.modal('hide');
    }
};