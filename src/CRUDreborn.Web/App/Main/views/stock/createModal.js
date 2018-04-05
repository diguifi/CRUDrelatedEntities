(function () {
    angular
        .module('app')
        .controller('app.views.stock.createModal', ['$scope', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.estoque',

            function ($scope, $uibModalInstance, produtoService, estoqueService) {
                var vm = this;
                vm.save = save;
                vm.cancel = cancel;
                vm.priceUp = priceUp;
                vm.priceDown = priceDown;
                vm.stockUp = stockUp;
                vm.stockDown = stockDown;

                vm.estoques = [];
                vm.estoque = {
                    stock: 0,
                    price: 0.0,
                    assignedProduct: []
                };
                vm.produtos = [];
                vm.produtosRight = [];
                vm.produto = {};

                getEstoque();

                function getEstoque() {
                    estoqueService.getAllEstoque({})
                        .then(function (result) {
                            vm.estoques = result.data.estoque;
                            getProdutos2();
                        });
                }

                function getProdutos2() {
                    produtoService.getAllProdutosCheckingEstoque({})
                        .then(function (result) {
                            console.log(result);
                            vm.produtos = result.data.produtos;
                        });
                }

                function save() {
                    vm.estoque.assignedProduct_Id = $scope.data.selector.id;
                    vm.estoque.assignedProduct = $scope.data.selector;
                    estoqueService.createEstoque(vm.estoque)
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