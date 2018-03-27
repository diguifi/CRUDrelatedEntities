(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.views.manufacturers.productsListing',
        ['$scope', '$uibModalInstance', 'abp.services.app.fabricante', 'id',

        function ($scope, $uibModalInstance, fabricanteService, id) {
            var vm = this;

            vm.produtos = {};

            activate();

            function activate() {
                fabricanteService.getAllAssignedProdutos(id)
                    .then(function (result) {
                        vm.produtos = result.data.produtos;
                        console.log(vm.produtos);
                    });
            }

            vm.cancel = function () {
                $uibModalInstance.dismiss({});
            };

        }
    ]);
})();