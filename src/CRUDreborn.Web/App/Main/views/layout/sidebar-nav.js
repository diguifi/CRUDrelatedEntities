(function () {
    var controllerId = 'app.views.layout.sidebarNav';
    angular.module('app').controller(controllerId, [
        '$rootScope', '$state', 'appSession',
        function ($rootScope, $state, appSession) {
            var vm = this;

            vm.menuItems = [
                createMenuItem(App.localize("HomePage"), "", "home", "home"),
                createMenuItem(App.localize("Sales"), "", "attach_money", "sales"),
                createMenuItem(App.localize("Stock"), "", "content_paste", "stock"),
                createMenuItem(App.localize("Products"), "", "shopping_basket", "products"),
                createMenuItem(App.localize("Manufacturers"), "", "business", "manufacturers"),
                
                
                createMenuItem(App.localize("System"), "", "menu", "", [
                    createMenuItem(App.localize("Users"), "Pages.Users", "people", "users"),
                    createMenuItem(App.localize("Roles"), "Pages.Roles", "local_offer", "roles"),
                    createMenuItem(App.localize("About"), "", "info", "about")
                ])
            ];

            vm.showMenuItem = function (menuItem) {
                if (menuItem.permissionName) {
                    return abp.auth.isGranted(menuItem.permissionName);
                }

                return true;
            }

            function createMenuItem(name, permissionName, icon, route, childItems) {
                return {
                    name: name,
                    permissionName: permissionName,
                    icon: icon,
                    route: route,
                    items: childItems
                };
            }
        }
    ]);
})();