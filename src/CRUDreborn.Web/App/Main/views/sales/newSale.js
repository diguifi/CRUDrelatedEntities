(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.sales.newSale',
        ['$scope', '$timeout', '$uibModal', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.estoque',

            function NewSalesController($scope, $timeout, $uibModal, $uibModalInstance, produtoService, estoqueService) {
                var vm = this;
                vm.refresh = refresh;
                vm.cancel = cancel;
                vm.nextSaleForm = nextSaleForm;
                
                vm.produto = {
                    name: '',
                    description: '',
                    assignedManufacturer: [],
                    consumable: false
                }

                vm.produtos = [];
                vm.estoque = [];

                getProdutos();
                getEstoque();


                function getEstoque() {
                    estoqueService.getAllEstoque({})
                        .then(function (result) {
                            vm.estoque = result.data.estoque;
                        });
                }

                function getProdutos() {
                    produtoService.getAllProdutos({})
                        .then(function (result) {
                            vm.produtos = result.data;
                        });
                }

                function refresh() {
                    getProdutos();
                };

                function cancel() {
                    $uibModalInstance.dismiss({});
                };

                function nextSaleForm(produto,estoque) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '/App/Main/views/sales/newSaleForm.cshtml',
                        controller: 'app.views.sales.newSaleForm as vm',
                        backdrop: 'static',
                        resolve: {
                            pid: function () {
                                return produto.id;
                            },
                            eid: function () {
                                return estoque.id;
                            }
                        }
                    });

                    modalInstance.rendered.then(function () {
                        cancel();
                        $.AdminBSB.input.activate();
                    });

                    modalInstance.result.then(function () {
                        getProdutos();
                    });
                };

            }
        ]);
})();