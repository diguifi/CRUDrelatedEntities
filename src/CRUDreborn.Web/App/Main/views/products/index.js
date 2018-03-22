(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.products.index', 
        ['$scope', '$timeout', '$uibModal', 'abp.services.app.produto',

        function ProductsController($scope, $timeout, $uibModal, produtoService) {
            var vm = this;
            vm.openProdutoCreationModal = openProdutoCreationModal;
            vm.openProdutoEditModal = openProdutoEditModal;
            vm.delete = Delete;
            vm.refresh = refresh;

            vm.produtos = [];

            getProdutos();

            function getProdutos() {
                console.log(produtoService.getAllProdutos());
                produtoService.getAllProdutos({}).then(function (result) {
                    vm.produtos = result.data.produtos;
                });
            }

            function openProdutoCreationModal() {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/manufacturers/createModal.cshtml',
                    controller: 'app.views.manufacturers.createModal as vm',
                    backdrop: 'static'
                });

                modalInstance.rendered.then(function () {
                    $.AdminBSB.input.activate();
                });

                modalInstance.result.then(function () {
                    getFabricantes();
                });
            };

            function openProdutoEditModal(fabricante) {
                var modalInstance = $uibModal.open({
                    templateUrl: '/App/Main/views/manufacturers/editModal.cshtml',
                    controller: 'app.views.manufacturers.editModal as vm',
                    backdrop: 'static',
                    resolve: {
                        id: function () {
                            return fabricante.id;
                        }
                    }
                });

                modalInstance.rendered.then(function () {
                    $timeout(function () {
                        $.AdminBSB.input.activate();
                    }, 0);
                });

                modalInstance.result.then(function () {
                    getFabricantes();
                });
            };

            function Delete(fabricante) {
                abp.message.confirm(
                    "Delete manufacturer '" + fabricante.name + "'?",
                    function (result) {
                        if (result) {
                            fabricanteService.deleteFabricante(fabricante.id)
                                .then(function () {
                                    abp.notify.info("Deleted user: " + fabricante.name);
                                    getFabricantes();
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