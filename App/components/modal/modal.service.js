'use strict';

function Modal($rootScope, $uibModal) {
    /**
     * Opens a modal
     * @param  {Object} scope      - an object to be merged with modal's scope
     * @param  {String} modalClass - (optional) class(es) to be applied to the modal
     * @return {Object}            - the instance $uibModal.open() returns
     */
    function openModal(scope, modalClass, controller) {
        if (!scope)
            scope = {};
        if (!modalClass)
            modalClass = 'modal-default';

        var modalScope = $rootScope.$new();

        angular.extend(modalScope, scope);

        return $uibModal.open({
            template: '/app/components/modal/modal.html',
            windowClass: modalClass,
            scope: modalScope,
            controller : controller,
            controllerAs : 'ctrl'
        });
    }

    // Public API here
    return {

        /* Confirmation modals */
        confirm: {


            input : function(name, cb){
                var args = Array.prototype.slice.call(arguments),
                    /*name = args.shift(),*/
                    inputModal;

                inputModal = openModal({
                    modal: {
                        dismissable: true,
                        input: true,
                        textarea: true,
                        title: 'Input a value',
                        html: '<p>Please provide an input for <strong>' + name + '</strong> </p>',
                        buttons: [{
                            classes: 'btn-default',
                            text: 'Cancelar',
                            click: function(e) {

                                inputModal.dismiss(e);
                            }
                        }]
                    }
                }, 'modal-success', ['$uibModalInstance',  function($uibModalInstance){
                    var $ctrl = this;
                    $ctrl.ok = function () {
                        $uibModalInstance.close($ctrl.input);
                    };
                }]);

                inputModal.result.then(function(input){
                    args[0] = input;
                    cb.apply(event, args);
                })

                /*inputModal.result.then(function(event) {
                  cb.apply(event, args);
                });*/

            },

            submit : function(name, cb){
                var args = Array.prototype.slice.call(arguments),
                    /*name = args.shift(),*/
                    submitModal;

                submitModal = openModal({
                    modal: {
                        dismissable: true,
                        title: 'Confirm',
                        html: '<p>Are you sure you want to publish <strong>' + name + '</strong> ?</p>',
                        buttons: [{
                            classes: 'btn-success',
                            text: 'Publicar',
                            click: function(e) {
                                submitModal.close(e);
                            }
                        }, {
                            classes: 'btn-default',
                            text: 'Cancelar',
                            click: function(e) {
                                submitModal.dismiss(e);
                            }
                        }]
                    }
                }, 'modal-success');

                submitModal.result.then(function(event) {
                    cb.apply(event, args);
                });
            },

            /**
             * Create a function to open a delete confirmation modal (ex. ng-click='myModalFn(name, arg1, arg2...)')
             * @param  {Function} del - callback, ran when delete is confirmed
             * @return {Function}     - the function to open the modal (ex. myModalFn)
             */
            delete : function(del) {
                if (!del)
                    del = angular.noop;
                /**
                 * Open a delete confirmation modal
                 * @param  {String} name   - name or info to show on modal
                 * @param  {All}           - any additional args are passed straight to del callback
                 */
                return function() {
                    var args = Array.prototype.slice.call(arguments),
                      name = args.shift(),
                      deleteModal;

                    deleteModal = openModal({
                        modal: {
                            dismissable: true,
                            title: 'Confirm Delete',
                            html: '<p>Are you sure you want to delete <strong>' + name + '</strong> ?</p>',
                            buttons: [{
                                classes: 'btn-danger',
                                text: 'Delete',
                                click: function(e) {
                                    deleteModal.close(e);
                                }
                            }, {
                                classes: 'btn-default',
                                text: 'Cancel',
                                click: function(e) {
                                    deleteModal.dismiss(e);
                                }
                            }]
                        }
                    }, 'modal-danger');

                    deleteModal.result.then(function(event) {
                        del.apply(event, args);
                    });
                };
            }
            }
        };
}

angular.module('UTRGVApp.modalService', [])
  .factory('Modal', ['$rootScope', '$uibModal', Modal])
  .name;
