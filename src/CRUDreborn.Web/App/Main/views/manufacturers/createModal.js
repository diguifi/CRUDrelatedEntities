(function () {
    angular
        .module('app')
        .controller('app.views.manufacturers.createModal', [
            '$scope', '$uibModalInstance', 'abp.services.app.fabricante',

            function ($scope, $uibModalInstance, fabricanteService) {
                var vm = this;
                vm.save = save
                vm.cancel = cancel

                vm.fabricante = {};

                function save() {
                    fabricanteService.createFabricante(vm.fabricante)
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