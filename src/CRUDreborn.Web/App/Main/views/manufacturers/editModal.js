(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.manufacturers.editModal',
        ['$scope', '$uibModalInstance', 'abp.services.app.fabricante', 'id',

        function ($scope, $uibModalInstance, fabricanteService, id) {
            var vm = this;
            vm.save = save;
            

            vm.fabricante = [];

            activate();

            function activate() {
                fabricanteService.getById(id)
                    .then(function (result) {
                        vm.fabricante = result.data;

                        fabricanteService.get(id)
                            .then(function (result) {
                                vm.fabricante = result.data;
                            });
                    });
            }

            function save() {
                fabricanteService.updateFabricante(vm.fabricante)
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