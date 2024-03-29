﻿(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.sales.index', ['$scope', '$timeout', '$uibModal', 'abp.services.app.venda', 'abp.services.app.produto',

            function SalesIndexController($scope, $timeout, $uibModal, vendaService, produtoService) {
                var vm = this;
                vm.openVendaCreationModal = openVendaCreationModal;
                vm.openDetailsModal = openDetailsModal;
                vm.delete = Delete;
                vm.refresh = refresh;

                vm.vendas = [];
                vm.produtos = [];

                getProdutos();
                getVendas();

                function getVendas() {
                    vendaService.getAllVendas({}).then(function (result) {
                        vm.vendas = result.data.vendas;
                    });
                }

                function getProdutos() {
                    produtoService.getAllProdutos({})
                        .then(function (result) {
                            vm.produtos = result.data;
                        });
                }

                function openVendaCreationModal() {
                    var modalInstance = $uibModal.open({
                        templateUrl: '/App/Main/views/sales/newSale.cshtml',
                        controller: 'app.views.sales.newSale as vm',
                        backdrop: 'static'
                    });

                    modalInstance.rendered.then(function () {
                        $.AdminBSB.input.activate();
                    });

                    modalInstance.result.then(function () {
                        getVendas();
                    });
                };

                function openDetailsModal(venda) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '/App/Main/views/sales/saleDetails.cshtml',
                        controller: 'app.views.sales.saleDetails as vm',
                        backdrop: 'static',
                        resolve: {
                            id: function () {
                                return venda.id;
                            }
                        }
                    });

                    modalInstance.rendered.then(function () {
                        $timeout(function () {
                            $.AdminBSB.input.activate();
                        }, 0);
                    });

                    modalInstance.result.then(function () {
                        getVendas();
                    });
                };

                function Delete(venda) {
                    abp.message.confirm(
                        "Delete sale from '" + venda.assignedProduct.name + "'?",
                        function (result) {
                            if (result) {
                                vendaService.deleteVenda(venda.id)
                                    .then(function () {
                                        abp.notify.info("Deleted sale: " + venda.assignedProduct.name);
                                        getVendas();
                                    });
                            }
                        });
                }

                function refresh() {
                    getVendas();
                };

            }
        ]);
})();