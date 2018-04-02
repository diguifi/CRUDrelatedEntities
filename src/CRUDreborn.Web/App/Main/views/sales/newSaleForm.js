(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.sales.newSaleForm',
        ['$scope', '$timeout', '$uibModal', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.estoque', 'abp.services.app.venda','pid', 'eid',

            function NewSalesController($scope, $timeout, $uibModal, $uibModalInstance, produtoService, estoqueService, vendaService, pid, eid) {
                var vm = this;
                vm.refresh = refresh;
                vm.cancel = cancel;
                vm.save = save;
                vm.calculateTotal = calculateTotal;
                vm.qntUp = qntUp;
                vm.qntDown = qntDown;
                vm.back = back;

                vm.venda = {
                    quantity: 0,
                    assignedProduct_Id: [],
                    total: 0.0
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
                            vm.venda.total = vm.estoque.price;
                        });
                }

                function calculateTotal() {
                    vm.venda.total = vm.venda.quantity * vm.estoque.price;
                }

                function qntUp() {
                    vm.venda.quantity++;
                }

                function qntDown() {
                    if (vm.venda.quantity > 0)
                        vm.venda.quantity--;
                }

                function save() {
                    vm.venda.assignedProduct = vm.produto;
                    vm.venda.assignedProduct_Id = vm.produto.id;
                    updateEstoque();
                    vendaService.createVenda(vm.venda)
                        .then(function () {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                        });
                };

                function updateEstoque() {
                    vm.estoque.stock -= vm.venda.quantity;
                    vm.estoque.assignedProduct_Id = vm.produto.id
                    estoqueService.updateEstoque(vm.estoque)
                        .then(function () {
                            abp.notify.info(App.localize('StockUpdated'));
                        });
                };

                function refresh() {
                    getProdutos();
                };

                function cancel() {
                    $uibModalInstance.dismiss({});
                };

                function back() {
                    var modalInstance = $uibModal.open({
                        templateUrl: '/App/Main/views/sales/newSale.cshtml',
                        controller: 'app.views.sales.newSale as vm',
                        backdrop: 'static'
                    });

                    modalInstance.rendered.then(function () {
                        cancel();
                        $.AdminBSB.input.activate();
                    });

                    modalInstance.result.then(function () {
                    });
                };

            }
        ]);
})();