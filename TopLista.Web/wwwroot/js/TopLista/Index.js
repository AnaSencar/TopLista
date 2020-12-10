Vue.use(window.vuelidate.default)
var { required, helpers, numeric } = window.validators

var top_lista = new Vue({
    el: "#top_lista",
    data: {
        showData: false,

        zapisi: [],
        user: {},

        initialZapis: {
            ime: '',
            prezime: '',
            sat: 0,
            min: 0,
            sec: 0
        },
        zapisForCreate: {},
        zapisToDelete: {},

        filter: { //dodano
            numberOfRecords: 10,
        }
    },
    validations: {
        zapisForCreate: {
            ime: { required },
            prezime: { required },
            sat: { numeric },
            min: { numeric },
            sec: { numeric },
        },
    },
    methods: {
        initialization() {
            this.getLoggedUser();
            this.getZapisi();
        },
        getZapisi() {
            axios
                .get('/api/Zapisi', {
                    params: {
                        selectApproved: true,
                        numberOfRecords: this.filter.numberOfRecords //dodano
                    }
                })
                .then(response => {
                    this.zapisi = response.data;
                    this.showData = true;
                });
        },
        getLoggedUser() {
            console.log("User");
            axios
                .get('/api/LoginApi')
                .then(response => {
                    this.user = response.data;
                })
        },
        createNewZapis() {
            this.zapisForCreate = _.cloneDeep(this.initialZapis);
            this.openCreateModal();
        },
        openCreateModal() {
            $('#createZapis').modal('show');
        },
        closeCreateModal() {
            $('#createZapis').modal('hide');
        },
        cancelCreationOfZapis() {
            this.closeCreateModal();
            this.zapisForCreate = {};
            this.resetValidations();
        },
        saveZapis() {
            this.$v.zapisForCreate.$touch();
            if (this.$v.zapisForCreate.$anyError) {
                return;
            }
            axios
                .post('/api/Zapisi', this.zapisForCreate)
                .then(response => {
                    toastr.success("Zapis uspjesno kreiran");
                    this.cancelCreationOfZapis();
                    this.getZapisi();
                })
                .catch(function (error) {
                    if (error.response) {
                        toastr.error("Neispravan unos");
                    }
                });
        },
        resetValidations() {
            this.$v.zapisForCreate.$reset();
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
                    this.getZapisi();
                })
                .catch(function (error) {
                    if (error.response.status == 404) {
                        toastr.error("Zapis nije pronadjen");
                    }
                });
        },
    },
    computed: {
        showTimeValidation() {
            if (this.$v.zapisForCreate.sat.$anyDirty || this.$v.zapisForCreate.min.$anyDirty || this.$v.zapisForCreate.sec.$anyDirty) {
                if (this.zapisForCreate.sat == 0 && this.zapisForCreate.min == 0 && this.zapisForCreate.sec == 0) {
                    return true;
                }
            }
            return false;
        }
    },
    created() {
        this.initialization();
    }
});