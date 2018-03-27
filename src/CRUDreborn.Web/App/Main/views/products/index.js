(function () {
    'use strict';

    angular
    .module('app')
    .controller('app.views.products.index', 
    ['$scope', '$timeout', '$uibModal', 'abp.services.app.produto', 'abp.services.app.fabricante',

        function ProductsController($scope, $timeout, $uibModal, produtoService, fabricanteService) {
            var vm = this;
            vm.openProdutoCreationModal = openProdutoCreationModal;
            vm.openProdutoEditModal = openProdutoEditModal;
            vm.delete = Delete;
            vm.refresh = refresh;

            vm.produtos = [];
            vm.produto = {
                name: '',
                description: '',
                assignedManufacturer: [],
                consumable: false
            }

            getFabricantes();
            getProdutos();
            

            function getProdutos() {
                produtoService.getAllProdutos({}).then(function (result) {
                    vm.produtos = result.data.produtos;
                });
            }

            function getFabricantes() {
                fabricanteService.getAllFabricantes({})
                    .then(function (result) {
                        vm.fabricantes = result.data;                        
                    });
            }

            function openProdutoCreationModal() {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/products/createModal.cshtml',
                    controller: 'app.views.products.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getProdutos();
                });
            };

            function openProdutoEditModal(produto) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/products/editModal.cshtml',
                    controller: 'app.views.products.editModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return produto.id;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $timeout(function () {
                        $.AdminBSB.input.activate();
                    }, 0);
                });

                modalInstance.result.then(function () {
                    getProdutos();
                });
            };

            function Delete(produto) {
                abp.message.confirm(
                    "Delete produto '" + produto.name + "'?",
                    function (result) {
                        if (result) {
                            produtoService.deleteProduto(produto.id)
                                .then(function () {
                                    abp.notify.info("Deleted produto: " + produto.name);
                                    getProdutos();
                                });
                        }
                    });
            }

            function refresh() {
                getProdutos();
            };

        }
    ]);
})();