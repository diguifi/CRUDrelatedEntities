(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.sales.index',
        ['$scope', '$timeout', '$uibModal', 'abp.services.app.venda',

            function ManufacturerController($scope, $timeout, $uibModal, vendaService) {
                var vm = this;
                vm.openVendaCreationModal = openVendaCreationModal;
                vm.openVendaEditModal = openVendaEditModal;
                vm.openDetailsModal = openDetailsModal;
                vm.delete = Delete;
                vm.refresh = refresh;

                vm.vendas = [];

                getVendas();

                function getVendas() {
                    vendaService.getAllVendas({}).then(function (result) {
                        vm.vendas = result.data.vendas;
                    });
                }

                function openVendaCreationModal() {
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

                function openVendaEditModal(fabricante) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '/App/Main/views/manufacturers/editModal.cshtml',
                        controller: 'app.views.manufacturers.editModal as vm',
                        backdrop: 'static',
                        resolve: {
                            id: function () {
                                return fabricante.id;
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

                function openDetailsModal(fabricante) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '/App/Main/views/manufacturers/productsListing.cshtml',
                        controller: 'app.views.manufacturers.productsListing as vm',
                        backdrop: 'static',
                        resolve: {
                            id: function () {
                                return fabricante.id;
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
                        "Delete manufacturer '" + fabricante.name + "'?",
                        function (result) {
                            if (result) {
                                fabricanteService.deleteFabricante(fabricante.id)
                                    .then(function () {
                                        abp.notify.info("Deleted user: " + fabricante.name);
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