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
                    creatorUserId: 0
                };
                vm.produto = {};

                activate();

                function activate() {
                    vendaService.getById(id)
                        .then(function (result) {
                            vm.venda = result.data;


                            var time = "";
                            for (var i=0; i<vm.venda.creationTime.length; i++){
                                if (vm.venda.creationTime[i] != "T")
                                    time+=vm.venda.creationTime[i];
                                else
                                    break;
                            }
                            vm.venda.creationTime = time;


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