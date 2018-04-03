(function () {
    'use strict';

    angular
    .module('app')
    .controller('app.views.products.editModal',
    ['$scope', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.fabricante', 'id',

        function ($scope, $uibModalInstance, produtoService, fabricanteService, id) {
            var vm = this;
            vm.save = save;
            vm.cancel = cancel;
            vm.setFabricante = setFabricante;

            vm.produto = [];

            vm.fabricantes = [];
            vm.fabricante = {};
            $scope.data = {};

            getFabricantes();
            activate();

            function getFabricantes() {
                fabricanteService.getAllFabricantes({}).then(function (result) {
                    vm.fabricantes = result.data.fabricantes;
                });
            }

            function setFabricante(fabricante) {
                vm.fabricante = fabricante;
                $scope.data.selector = vm.fabricante;
            }

            function activate() {
                produtoService.getById(id)
                    .then(function (result) {
                        vm.produto = result.data;
                        fabricanteService.getById(vm.produto.assignedManufacturer_Id)
                            .then(function (result) {
                                vm.produto.assignedManufacturer = result.data;
                                setFabricante(vm.produto.assignedManufacturer);
                            });
                    });
            }

            function save() {
                vm.produto.assignedManufacturer = $scope.data.selector;
                vm.produto.assignedManufacturer_Id = $scope.data.selector.id;
                produtoService.updateProduto(vm.produto)
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