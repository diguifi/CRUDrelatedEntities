(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.stock.index',
        ['$scope', '$timeout', '$uibModal', 'abp.services.app.estoque', 'abp.services.app.produto',

            function ProductsController($scope, $timeout, $uibModal, estoqueService, produtoService) {
                var vm = this;
                vm.openEstoqueCreationModal = openEstoqueCreationModal;
                vm.openEstoqueEditModal = openEstoqueEditModal;
                vm.deleteEstoque = Delete;
                vm.refresh = refresh;

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

                function openEstoqueCreationModal() {
                    var modalInstance = $uibModal.open({
                        templateUrl: '/App/Main/views/stock/createModal.cshtml',
                        controller: 'app.views.stock.createModal as vm',
                        backdrop: 'static'
                    });

                    modalInstance.rendered.then(function () {
                        $.AdminBSB.input.activate();
                    });

                    modalInstance.result.then(function () {
                        getEstoque();
                    });
                };

                function openEstoqueEditModal(estoque) {
                    var modalInstance = $uibModal.open({
                        templateUrl: '/App/Main/views/stock/editModal.cshtml',
                        controller: 'app.views.stock.editModal as vm',
                        backdrop: 'static',
                        resolve: {
                            id: function () {
                                return estoque.id;
                            }
                        }
                    });

                    modalInstance.rendered.then(function () {
                        $timeout(function () {
                            $.AdminBSB.input.activate();
                        }, 0);
                    });

                    modalInstance.result.then(function () {
                        getEstoque();
                    });
                };

                function Delete(estoque) {
                    abp.message.confirm(
                        "Remover produto '" + estoque.assignedProduct.name + "' da lista de estoque?",
                        function (result) {
                            if (result) {
                                estoqueService.deleteEstoque(estoque.id)
                                    .then(function () {
                                        abp.notify.info("Registro de estoque para: " + estoque.assignedProduct.name + " removido");
                                        getEstoque();
                                    });
                            }
                        });
                }

                function refresh() {
                    getEstoque();
                };

            }
        ]);
})();