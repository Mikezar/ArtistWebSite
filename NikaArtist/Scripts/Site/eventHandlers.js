$(document).ready(function () {
	$(document).on('click', '#selectCover', function () {
		GetPaintingSelectList(
			function (response) {
				ModalHelper.modalOpen('#painting', response);
				$('tr').on('click', function () {
					var id = this.id;
					var img = $("#cover").find('img')[0];
					img.src = '/image?id=' + id;
					$("#CoverId").val(id);
					$('#paintingModal').modal("toggle");
				});
			});
		});
});