var layout = new Vue({
    el: "#layout",
    data: {
        showData: false,
        user: {},
    },
    methods: {
        getLoggedUser() {
            axios
                .get('/api/LoginApi')
                .then(response => {
                    this.user = response.data;
                    this.showData = true;
                })
        },
        logOut() {
            axios
                .delete('/api/LoginApi')
                .then(response => {
                    this.getLoggedUser();
                    window.location = "/";
                });
        },
        checkIfAdminTab() {
            if (window.location.href.toLowerCase().includes("admin")) {
                return "nav-item active";
            }
            return "nav-item";
        },
        checkIndexTab() {
            if (window.location.pathname === "/") {
                return "nav-item active";
            }
            return "nav-item";
        }
    },
    created() {
        this.getLoggedUser();
    }
});