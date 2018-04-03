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
            vm.totalSold = 0;
            vm.topMostSold = "TOP MOST SOLD";

            getProdutos();
            

            function getProdutos() {
                produtoService.getAllProdutos({}).then(function (result) {
                    var produtos = result.data.produtos;
                    vm.qtdProdutos = produtos.length;
                    getVendas();
                });
            }

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
                            vm.totalSold+=vm.mostSoldJSON[i].Value;
                        }
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

                var realtime = 'on';
                function initRealTimeChart() {
                    //Real time ==========================================================================================
                    var plot = $.plot('#real_time_chart', [getRandomData()], {
                        series: {
                            shadowSize: 0,
                            color: 'rgb(0, 188, 212)'
                        },
                        grid: {
                            borderColor: '#f3f3f3',
                            borderWidth: 1,
                            tickColor: '#f3f3f3'
                        },
                        lines: {
                            fill: true
                        },
                        yaxis: {
                            min: 0,
                            max: 100
                        },
                        xaxis: {
                            min: 0,
                            max: 100
                        }
                    });

                    function updateRealTime() {
                        plot.setData([getRandomData()]);
                        plot.draw();

                        var timeout;
                        if (realtime === 'on') {
                            timeout = setTimeout(updateRealTime, 320);
                        } else {
                            clearTimeout(timeout);
                        }
                    }

                    updateRealTime();

                    $('#realtime').on('change', function () {
                        realtime = this.checked ? 'on' : 'off';
                        updateRealTime();
                    });
                    //====================================================================================================
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
                                value: parseFloat(( (vm.totalSold - vm.mostSoldJSON[0].Value) / vm.totalSold) * 100).toFixed(2)
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

                var data = [], totalPoints = 110;
                function getRandomData() {
                    if (data.length > 0)
                        data = data.slice(1);

                    while (data.length < totalPoints) {
                        var prev = data.length > 0 ? data[data.length - 1] : 50, y = prev + Math.random() * 10 - 5;

                        if (y < 0) {
                            y = 0;
                        }
                        else if (y > 100) {
                            y = 100;
                        }

                        data.push(y);
                    }

                    var res = [];
                    for (var i = 0; i < data.length; ++i) {
                        res.push([i, data[i]]);
                    }

                    return res;
                }

                initSummaries();
                initRealTimeChart();
                initDonutChart();
                initSparkline();
            };
        }
    ]);
})();