//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

(function () {
    'use strict';

    var controllerId = 'ElementCellEditController';
    angular.module('main')
        .controller(controllerId, ['elementCellFactory',
            'elementFieldFactory',
            'elementItemFactory',
            'logger',
            '$location',
            '$routeParams',
            ElementCellEditController]);

    function ElementCellEditController(elementCellFactory,
		elementFieldFactory,
		elementItemFactory,
		logger,
		$location,
		$routeParams) {
        logger = logger.forSource(controllerId);

        var isNew = $location.path() === '/manage/generated/elementCell/new';
        var isSaving = false;

        // Controller methods (alphabetically)
        var vm = this;
        vm.elementFieldSet = [];
        vm.elementItemSet = [];
        vm.cancelChanges = cancelChanges;
        vm.isSaveDisabled = isSaveDisabled;
        vm.entityErrors = [];
        vm.elementCell = null;
        vm.saveChanges = saveChanges;
        vm.hasChanges = hasChanges;

        initialize();

        /*** Implementations ***/

        function cancelChanges() {

            $location.path('/manage/generated/elementCell');

            //if (elementCellFactory.hasChanges()) {
            //    elementCellFactory.rejectChanges();
            //    logWarning('Discarded pending change(s)', null, true);
            //}
        }

        function hasChanges() {
            return elementCellFactory.hasChanges();
        }

        function initialize() {

            elementFieldFactory.getElementFieldSet(false)
                .then(function (data) {
                    vm.elementFieldSet = data;
                });

            elementItemFactory.getElementItemSet(false)
                .then(function (data) {
                    vm.elementItemSet = data;
                });

            if (isNew) {
                // TODO For development enviroment, create test entity?
            }
            else {
                elementCellFactory.getElementCell($routeParams.Id)
                    .then(function (data) {
                        vm.elementCell = data;
                    })
                    .catch(function (error) {
                        // TODO User-friendly message?
                    });
            }
        }

        function isSaveDisabled() {
            return isSaving ||
                (!isNew && !elementCellFactory.hasChanges());
        }

        function saveChanges() {

            if (isNew) {
                elementCellFactory.createElementCell(vm.elementCell);
            }

            isSaving = true;
            elementCellFactory.saveChanges()
                .then(function (result) {
                    $location.path('/manage/generated/elementCell');
                })
                .catch(function (error) {
                    // Conflict (Concurrency exception)
                    if (typeof error.status !== 'undefined' && error.status === '409') {
                        // TODO Try to recover!
                    } else if (typeof error.entityErrors !== 'undefined') {
                        vm.entityErrors = error.entityErrors;
                    }
                })
                .finally(function () {
                    isSaving = false;
                });
        }
    }
})();
