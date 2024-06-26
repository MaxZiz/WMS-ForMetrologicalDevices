﻿function openModal(parameters){
    const id = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');


     $.ajax({ 
        type: 'GET',
         url: url,
        data: { "id": id },
         success: function (response) {
             $('.modal-dialog').removeClass("modal-lg");
             modal.find(".modal-body").html(response);
             modal.modal('show')
             console.log(id);
        },
        failure: function () {
            modal.modal('hide')
            console.log('fail');
        },
       
    });
};


