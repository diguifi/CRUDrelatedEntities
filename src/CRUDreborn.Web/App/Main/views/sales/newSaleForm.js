(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.sales.newSaleForm',
        ['$scope', '$timeout', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.estoque', 'abp.services.app.venda','pid', 'eid',

            function NewSalesController($scope, $timeout, $uibModalInstance, produtoService, estoqueService, vendaService, pid, eid) {
                var vm = this;
                vm.refresh = refresh;
                vm.cancel = cancel;
                vm.calculateTotal = calculateTotal;

                vm.venda = {
                    quantity: 0,
                    assignedProduct_Id: [],
                    total: 0
                };
                vm.estoque = {
                    stock: 0,
                    price: 0,
                    assignedProduct_Id: 0,
                    assignedProduct: [],
                    consumable: false
                };
                vm.produto = {
                    name: '',
                    description: '',
                    assignedManufacturer: [],
                    consumable: false
                };
                
                getProduto();
                getEstoque();

                function getProduto() {
                    produtoService.getById(pid)
                        .then(function (result) {
                            vm.produto = result.data;
                        });
                }

                function getEstoque() {
                    estoqueService.getById(eid)
                        .then(function (result) {
                            vm.estoque = result.data;
                        });
                }

                function calculateTotal() {
                    //if(<=)
                        vm.venda.total = vm.venda.quantity * vm.estoque.price;
                    console.log(vm.venda.total);
                }

                function save() {
                    calculateTotal();
                    vm.venda.assignedProduct = vm.produto;
                    vendaService.createVenda(vm.venda)
                        .then(function () {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                        });
                };

                function refresh() {
                    getProdutos();
                };

                function cancel() {
                    $uibModalInstance.dismiss({});
                };

            }
        ]);
})();