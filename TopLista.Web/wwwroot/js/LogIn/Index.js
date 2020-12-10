Vue.use(window.vuelidate.default)
var { required, helpers, numeric } = window.validators

var login = new Vue({
    el: "#login",
    data: {
        loginData: {
            username: null,
            password: null,
        }
    },
    validations: {
        loginData: {
            username: { required },
            password: { required },
        },
    },
    methods: {
        logIn() {
            this.$v.loginData.$touch();
            if (this.$v.loginData.$anyError) {
                return;
            }
            axios
                .post('/api/LoginApi', this.loginData)
                .then(response => {
                    window.location = '/Admin/Index';
                })
                .catch(function (error) {
                    if (error.response.status == 400) {
                        toastr.error("Neispravan unos");
                    }
                    else if (error.response.status == 401) {
                        toastr.error("Neuspjeli login");
                    }
                });
        },
    },
    created() {
    }
});