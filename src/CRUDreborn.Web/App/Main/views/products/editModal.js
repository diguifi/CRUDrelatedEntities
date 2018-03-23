(function () {
    'use strict';

    angular
    .module('app')
    .controller('app.views.products.editModal',
    ['$scope', '$uibModalInstance', 'abp.services.app.produto', 'abp.services.app.fabricante', 'id',

        function ($scope, $uibModalInstance, produtoService, fabricanteService, id) {
            var vm = this;
            vm.save = save;
            

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

            function activate() {
                produtoService.getById(id)
                    .then(function (result) {
                        vm.produto = result.data;
                        console.log(vm.produto)

                        produtoService.get(id)
                            .then(function (result) {
                                vm.produto = result.data;
                                console.log(vm.produto)
                            });
                    });
            }

            function save() {
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