(function () {
    angular
        .module('app')
        .controller('app.views.stock.createModal',
        ['$scope', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.estoque',

            function ($scope, $uibModalInstance, produtoService, estoqueService) {
                var vm = this;
                vm.save = save;
                vm.cancel = cancel;

                vm.estoque = {
                    stock: 0,
                    price: 0.0,
                    assignedProduct: []
                };
                vm.produtos = [];
                vm.produto = {};

                getProdutos();

                function getProdutos() {
                    produtoService.getAllFabricantes({}).then(function (result) {
                        vm.produtos = result.data.produtos;
                    });
                }

                function save() {
                    vm.estoque.assignedProduct = vm.produto;
                    estoqueService.createEstoque(vm.estoque)
                        .then(function () {
                            abp.notify.info(App.localize('SavedSuccessfully'));
                            $uibModalInstance.close();
                        });
                };

                function cancel() {
                    $uibModalInstance.dismiss({});
                };

            }
        ]);
})();