(function () {
    angular
        .module('app')
        .controller('app.views.stock.editModal', ['$scope', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.estoque', 'id',

            function ($scope, $uibModalInstance, produtoService, estoqueService, id) {
                var vm = this;
                vm.save = save;
                vm.cancel = cancel;
                vm.setProduto = setProduto;
                vm.priceUp = priceUp;
                vm.priceDown = priceDown;
                vm.stockUp = stockUp;
                vm.stockDown = stockDown;

                vm.estoque = {
                    stock: 0,
                    price: 0.0,
                    assignedProduct: []
                };

                vm.produtos = [];
                vm.produto = {};
                $scope.data = {};

                getProdutos();
                activate();

                function getProdutos() {
                    produtoService.getAllProdutos({}).then(function (result) {
                        vm.produtos = result.data.produtos;
                    });
                }

                function setProduto(produto) {
                    vm.produto = produto;
                    $scope.data.selector = vm.produto;
                }

                function activate() {
                    estoqueService.getById(id)
                        .then(function (result) {
                            vm.estoque = result.data;
                            produtoService.getById(vm.estoque.assignedProduct_Id)
                                .then(function (result) {
                                    vm.estoque.assignedProduct = result.data;
                                    setProduto(vm.estoque.assignedProduct);
                                });
                        });
                }

                function save() {
                    vm.estoque.assignedProduct = $scope.data.selector;
                    vm.estoque.assignedProduct_Id = $scope.data.selector.id;
                    estoqueService.updateEstoque(vm.estoque)
                        .then(function () {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                        });
                };

                function cancel() {
                    $uibModalInstance.dismiss({});
                };

                function priceUp() {
                    vm.estoque.price++;
                }

                function priceDown() {
                    if (vm.estoque.price > 0)
                        vm.estoque.price--;
                }

                function stockUp() {
                    vm.estoque.stock++;
                }

                function stockDown() {
                    if (vm.estoque.stock > 0)
                        vm.estoque.stock--;
                }

            }
        ]);
})();