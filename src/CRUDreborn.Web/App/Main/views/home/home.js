(function () {
    var controllerId = 'app.views.home';
    angular.module('app').controller(controllerId, [
        '$scope', 'abp.services.app.produto', 'abp.services.app.venda', 'abp.services.app.estoque',
        function ($scope, produtoService, vendaService, estoqueService) {
            var vm = this;

            vm.qtdProdutos = 0;
            vm.qtdVendas = 0;
            vm.total = 0;
            vm.emptyStocks = 0;
            vm.mostSoldJSON = [];
            vm.daysSalesJSON = {};
            vm.totalSold = 0;
            vm.topMostSold = "TOP MOST SOLD";

            getVendas();

            function getVendas() {
                vendaService.getAllVendas({}).then(function (result) {
                    var vendas = result.data.vendas;
                    vm.qtdVendas = vendas.length;
                    getTotal();
                });
            }

            function getTotal() {
                vendaService.getTotalVendas()
                    .then(function (result) {
                        vm.total = result.data;
                        getProdutos();
                    });
            }

            function getProdutos() {
                produtoService.getAllProdutos({}).then(function (result) {
                    var produtos = result.data.produtos;
                    vm.qtdProdutos = produtos.length;
                    getEmptyStocks();
                });
            }

            function getEmptyStocks() {
                estoqueService.getAllEmptyEstoque()
                    .then(function (result) {
                        vm.emptyStocks = result.data;
                        getMostSold();
                    });
            }

            function getMostSold() {
                vendaService.getMostSold()
                    .then(function (result) {
                        vm.mostSoldJSON = JSON.parse(result.data);
                        for (var i = 0; i < vm.mostSoldJSON.length; i++) {
                            vm.totalSold += vm.mostSoldJSON[i].Value;
                        }
                        getDaysSales();
                    });
            }

            function getDaysSales() {
                vendaService.getDaysSales()
                    .then(function (result) {
                        vm.daysSalesJSON = JSON.parse(result.data);
                        console.log(vm.daysSalesJSON);
                        init();
                    });
            }

            function init() {

                function initSummaries() {
                    //Widgets count
                    $('.count-to').countTo();

                    //Sales count to
                    $('.sales-count-to').countTo({
                        formatter: function (value, options) {
                            return '$' + value.toFixed(2).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, ' ').replace('.', ',');
                        }
                    });
                }

                function initSparkline() {
                    $(".sparkline").each(function () {
                        var $this = $(this);
                        $this.sparkline('html', $this.data());
                    });
                }

                //Top most sold Donut Chart
                function initDonutChart() {
                    //in case theres less than 4 products sold, olny show the top seller compared to all others
                    if (vm.qtdVendas < 4) {
                        window.Morris.Donut({
                            element: 'donut_chart',
                            data: [{
                                label: vm.mostSoldJSON[0].Key,
                                value: parseFloat((vm.mostSoldJSON[0].Value / vm.totalSold) * 100).toFixed(2)
                            }, {
                                label: 'Other',
                                value: parseFloat(((vm.totalSold - vm.mostSoldJSON[0].Value) / vm.totalSold) * 100).toFixed(2)
                            }],
                            colors: ['rgb(233, 30, 99)', 'rgb(96, 125, 139)'],
                            formatter: function (y) {
                                return y + '%';
                            }
                        });
                    }
                    //if theres 4 or more products sold
                    else {
                        //case the whole sum of all other products is too high, wont plot "others", just the top sellers
                        if ((((vm.totalSold - (vm.mostSoldJSON[0].Value + vm.mostSoldJSON[1].Value + vm.mostSoldJSON[2].Value + vm.mostSoldJSON[3].Value)) / vm.totalSold) * 100) > 60) {
                            vm.topMostSold = "TOP 4 MOST SOLD";
                            window.Morris.Donut({
                                element: 'donut_chart',
                                data: [{
                                    label: vm.mostSoldJSON[0].Key,
                                    value: parseFloat((vm.mostSoldJSON[0].Value / vm.totalSold) * 100).toFixed(2)
                                }, {
                                    label: vm.mostSoldJSON[1].Key,
                                    value: parseFloat((vm.mostSoldJSON[1].Value / vm.totalSold) * 100).toFixed(2)
                                }, {
                                    label: vm.mostSoldJSON[2].Key,
                                    value: parseFloat((vm.mostSoldJSON[2].Value / vm.totalSold) * 100).toFixed(2)
                                }, {
                                    label: vm.mostSoldJSON[3].Key,
                                    value: parseFloat((vm.mostSoldJSON[3].Value / vm.totalSold) * 100).toFixed(2)
                                }],
                                colors: ['rgb(233, 30, 99)', 'rgb(0, 188, 212)', 'rgb(255, 152, 0)', 'rgb(0, 150, 136)'],
                                formatter: function (y) {
                                    return y + '%';
                                }
                            });
                        }
                        //plot the top 4 sold products
                        else {
                            vm.topMostSold = "TOP 4 MOST SOLD";
                            window.Morris.Donut({
                                element: 'donut_chart',
                                data: [{
                                    label: vm.mostSoldJSON[0].Key,
                                    value: parseFloat((vm.mostSoldJSON[0].Value / vm.totalSold) * 100).toFixed(2)
                                }, {
                                    label: vm.mostSoldJSON[1].Key,
                                    value: parseFloat((vm.mostSoldJSON[1].Value / vm.totalSold) * 100).toFixed(2)
                                }, {
                                    label: vm.mostSoldJSON[2].Key,
                                    value: parseFloat((vm.mostSoldJSON[2].Value / vm.totalSold) * 100).toFixed(2)
                                }, {
                                    label: vm.mostSoldJSON[3].Key,
                                    value: parseFloat((vm.mostSoldJSON[3].Value / vm.totalSold) * 100).toFixed(2)
                                }, {
                                    label: 'Other',
                                    value: parseFloat(((vm.totalSold - (vm.mostSoldJSON[0].Value + vm.mostSoldJSON[1].Value + vm.mostSoldJSON[2].Value + vm.mostSoldJSON[3].Value)) / vm.totalSold) * 100).toFixed(2)
                                }],
                                colors: ['rgb(233, 30, 99)', 'rgb(0, 188, 212)', 'rgb(255, 152, 0)', 'rgb(0, 150, 136)', 'rgb(96, 125, 139)'],
                                formatter: function (y) {
                                    return y + '%';
                                }
                            });
                        }
                    }
                }

                new Morris.Line({
                    // ID of the element in which to draw the chart.
                    element: 'salesChart',
                    // Chart data records -- each entry in this array corresponds to a point on
                    // the chart.
                    data: vm.daysSalesJSON,
                    // The name of the data record attribute that contains x-values.
                    xkey: 'Key',
                    // A list of names of data record attributes that contain y-values.
                    ykeys: ['Value'],
                    // Labels for the ykeys -- will be displayed when you hover over the
                    // chart.
                    labels: ['Sales'],
                    xLabels: ['day']
                });

                initSummaries();
                initDonutChart();
                initSparkline();
            };
        }
    ]);
})();