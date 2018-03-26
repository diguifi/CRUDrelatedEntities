(function () {
    'use strict';

    angular
    .module('app')
    .controller('app.views.products.editModal',
    ['$scope', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.fabricante', 'id',

        function ($scope, $uibModalInstance, produtoService, fabricanteService, id) {
            var vm = this;
            vm.save = save;
            vm.setFabricante = setFabricante;
            

            vm.produto = [];

            vm.fabricantes = [];
            vm.fabricante = {};

            getFabricantes();
            activate();

            function getFabricantes() {
                fabricanteService.getAllFabricantes({}).then(function (result) {
                    vm.fabricantes = result.data.fabricantes;
                });
            }

            function setFabricante(fabricante) {
                vm.fabricante = fabricante;
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
                vm.produto.assignedManufacturer_Id = vm.fabricante.id
                produtoService.updateProduto(vm.produto)
                    .then(function () {
                        abp.notify.info(App.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    });
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

        }
    ]);
})();