(function () {
    'use strict';

    var app = angular.module('app', [
        'ngAnimate',
        'ngSanitize',

        'ui.router',
        'ui.bootstrap',
        'ui.jq',

        'abp'
    ]);

    //Configuration for Angular UI routing.
    app.config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider', '$qProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider, $qProvider) {
            $locationProvider.hashPrefix('');
            $urlRouterProvider.otherwise('/');
            $qProvider.errorOnUnhandledRejections(false);

            if (abp.auth.hasPermission('Pages.Users')) {
                $stateProvider
                    .state('users', {
                        url: '/users',
                        templateUrl: '/App/Main/views/users/index.cshtml',
                        menu: 'Users' //Matches to name of 'Users' menu in CRUDrebornNavigationProvider
                    });
                $urlRouterProvider.otherwise('/users');
            }

            if (abp.auth.hasPermission('Pages.Roles')) {
                $stateProvider
                    .state('roles', {
                        url: '/roles',
                        templateUrl: '/App/Main/views/roles/index.cshtml',
                        menu: 'Roles' //Matches to name of 'Tenants' menu in CRUDrebornNavigationProvider
                    });
                $urlRouterProvider.otherwise('/roles');
            }

            if (abp.auth.hasPermission('Pages.Tenants')) {
                $stateProvider
                    .state('tenants', {
                        url: '/tenants',
                        templateUrl: '/App/Main/views/tenants/index.cshtml',
                        menu: 'Tenants' //Matches to name of 'Tenants' menu in CRUDrebornNavigationProvider
                    });
                $urlRouterProvider.otherwise('/tenants');
            }

            $stateProvider
                .state('home', {
                    url: '/',
                    templateUrl: '/App/Main/views/home/home.cshtml',
                    menu: 'Home' //Matches to name of 'Home' menu in CRUDrebornNavigationProvider
                })
                .state('about', {
                    url: '/about',
                    templateUrl: '/App/Main/views/about/about.cshtml',
                    menu: 'About' //Matches to name of 'About' menu in CRUDrebornNavigationProvider
                })
                .state('manufacturers', {
                    url: '/manufacturers',
                    templateUrl: '/App/Main/views/manufacturers/index.cshtml',
                    menu: 'Manufacturers'
                })
                .state('products', {
                    url: '/products',
                    templateUrl: '/App/Main/views/products/index.cshtml',
                    menu: 'Products'
                })
                .state('stock', {
                    url: '/stock',
                    templateUrl: '/App/Main/views/stock/index.cshtml',
                    menu: 'Stock'
                })
                .state('sales', {
                    url: '/sales',
                    templateUrl: '/App/Main/views/sales/index.cshtml',
                    menu: 'Sales'
                });
        }
    ]);

})();