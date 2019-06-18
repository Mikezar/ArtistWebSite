function DeleteEntity(id, url, element) {
    AjaxHelper.post(url, { id: id }, function (result) {
        if (result.success) {
            $(element).closest('tr').fadeIn(500,
                function () {
                    $(this).remove();
                });
        } else {
			bootbox.alert(result.errorMessage);
			ModalHelper.removeBackDrop();
        }
    });
}

function AddEditEntity(id, url, element) {
    AjaxHelper.get(url, { id: id },
        function (response) {
            ModalHelper.modalOpen(element, response);
        });
}

var AjaxHelper = (function () {
    return {
        get: function (url, data, successCallback) {
            $.ajax({
                type: 'GET',
                url: url,
                data: data,
                success: function (response) {
                    if (successCallback) {
                        successCallback(response);
                    }
                }
            });
        },
        post: function (url, data, successCallback, processData, contentType) {

            if (processData === undefined) {
                processData = true;
            }

            if (contentType === undefined) {
                contentType = 'application/x-www-form-urlencoded';
            }

            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                cache: false,
                processData: processData,
                contentType: contentType,
                success: function (response) {
                    if (successCallback) {
                        successCallback(response);
                    }
                }
            });
        }
    };
})();

function SortHandler(url) {
	$('tr').on('click', function () {
		$('tr').removeClass('active-row');
		$(this).addClass('active-row');
	});

	var id, sid, row;

	$('.down').on('click', function () {
		id = $(this).data('painting');
		sid = $(this).closest('tr').next().find('.down').data('painting');
		row = $(this).closest('tr');
		row.insertAfter(row.next());

		ChangeOrder(id, sid, url);
	});

	$('.up').on('click', function () {
		id = $(this).data('painting');
		row = $(this).closest('tr');
		sid = row.prev().find('.up').data('painting');
		row.prev().insertAfter(row);

		ChangeOrder(id, sid, url);
	});

	function ChangeOrder(id, sid, url) {
		var data = {
			CurrentId: id,
			SwappedId: sid
		};

		AjaxHelper.post(url, data);
	}
}

function ResponseHandler(response) {

	var json = response.responseJSON;
	if (!json.success) {
		bootbox.alert(json.errorMessage);
		ModalHelper.removeBackDrop();
	}
	else {
		$('.modal-header').click();
		$('.modal').click();
	}
}

function GetPaintingSelectList(callback) {
	AjaxHelper.get('/Management/GetPaintingsSelectList', null,
		function (response) {
			if (callback)
				callback(response);
		});
}


var ModalHelper = (function () {
    return {
        modalOpen: function (el, response) {
            $(el).html(response);
            $(el + 'Modal').modal('show');
			this.removeBackDrop();
		},
		removeBackDrop: function () {
			var backdrop = $('.modal-backdrop');
			backdrop.removeClass('modal-backdrop');
			backdrop.addClass('modal');
		},
        onSuccessSave: function (el) {
            $(el).parents('.modal').modal("toggle");
        },
        initForm: function (id) {
            var form = $('#' + id)
                .removeData("validator")
                .removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse(form);
        }
    };
})();


var Generator = (function () {
	return {
		giveImagePlaceholder: function () {
			return 'https://via.placeholder.com/220x141/FFFFFF/000000/?text=' + window.location.host;
		}
	};
})();