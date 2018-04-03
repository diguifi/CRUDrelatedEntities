(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.sales.saleDetails',
        ['$scope', '$uibModalInstance', 'abp.services.app.venda', 'abp.services.app.produto', 'id',

            function SaleController($scope, $uibModalInstance, vendaService, produtoService, id) {
                var vm = this;
                vm.cancel = cancel;

                vm.venda = {
                    assignedProduct_Id: 0,
                    assignedProduct: [],
                    quantity: 0,
                    total: 0.0,
                    creationTime: 0,
                    creatorUserId: 0,
                    date: ""
                };
                vm.produto = {};

                activate();

                function activate() {
                    vendaService.getById(id)
                        .then(function (result) {
                            vm.venda = result.data;
                            produtoService.getById(vm.venda.assignedProduct_Id)
                                .then(function (result) {
                                    vm.venda.assignedProduct = result.data;
                                    setProduto(vm.venda.assignedProduct);
                                });
                        });
                }

                function setProduto(produto) {
                    vm.produto = produto;
                }

                function cancel() {
                    $uibModalInstance.dismiss({});
                };

            }
        ]);
})();