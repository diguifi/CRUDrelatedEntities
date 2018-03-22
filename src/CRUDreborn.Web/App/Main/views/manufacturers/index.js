(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.manufacturers.index', 
        ['$scope', '$timeout', '$uibModal', 'abp.services.app.fabricante',

        function ManufacturerController($scope, $timeout, $uibModal, fabricanteService) {
            var vm = this;
            vm.openFabricanteCreationModal = openFabricanteCreationModal;
            vm.openFabricanteEditModal = openFabricanteEditModal;
            vm.Delete = Delete;
            vm.refresh = refresh;

            vm.fabricantes = [];

            getFabricantes();

            function getFabricantes() {
                fabricanteService.getAllFabricantes({}).then(function (result) {
                    console.log(result.data.fabricantes);
                    vm.fabricantes = result.data.fabricantes;
                });
            }

            function openFabricanteCreationModal() {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/manufacturers/createModal.cshtml',
                    controller: 'app.views.manufacturers.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getFabricantes();
                });
            };

            function openFabricanteEditModal(fabricante) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/users/editModal.cshtml',
                    controller: 'app.views.users.editModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return user.id;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $timeout(function () {
                        $.AdminBSB.input.activate();
                    }, 0);
                });

                modalInstance.result.then(function () {
                    getFabricantes();
                });
            };

            function Delete(fabricante) {
                abp.message.confirm(
                    "Delete manufacturer '" + fabricante.Name + "'?",
                    function (result) {
                        if (result) {
                            fabricanteService.delete({ id: fabricante.id })
                                .then(function () {
                                    abp.notify.info("Deleted user: " + fabricante.Name);
                                    getFabricantes();
                                });
                        }
                    });
            }

            function refresh() {
                getFabricantes();
            };

        }
    ]);
})();