var admin = new Vue({
    el: "#admin",
    data: {
        showData: false,

        unapprovedZapisi: [],
        zapisToDelete: {},
    },
    methods: {
        initialization() {
            this.getUnapprovedZapisi();
        },
        getUnapprovedZapisi() {
            axios
                .get('/api/Zapisi', {
                    params: {
                        selectApproved: false
                    }
                })
                .then(response => {
                    this.unapprovedZapisi = response.data;
                    this.showData = true;
                });
        },
        approveZapis(zapis) {
            zapis.odobreno = true;
            axios
                .patch('/api/Zapisi/' + zapis.id, zapis)
                .then(response => {
                    toastr.success("Zapis uspjesno odobren");
                    this.initialization();
                })
                .catch(function (error) {
                    if (error.response.status == 404) {
                        toastr.error("Zapis nije pronadjen");
                    }
                });
        },
        denyZapisClick(zapis) {
            this.zapisToDelete = zapis;
            this.openDeleteModal();
        },
        openDeleteModal() {
            $('#denyZapis').modal('show');
        },
        closeDeleteModal() {
            $('#denyZapis').modal('hide');
        },
        cancelDenyZapisClicked() {
            this.closeDeleteModal();
            this.zapisToDelete = {};
        },
        denyZapis() {
            axios
                .delete('/api/Zapisi/' + this.zapisToDelete.id)
                .then(response => {
                    toastr.success("Zapis uspjesno obrisan");
                    this.cancelDenyZapisClicked();
                    this.initialization();
                })
                .catch(function (error) {
                    if (error.response.status == 404) {
                        toastr.error("Zapis nije pronadjen");
                    }
                });
        },
    },
    created() {
        this.initialization();
    }
});