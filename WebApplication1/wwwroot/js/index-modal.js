(function ($) {
	function User() {
		var $this = this;

		function initilizeModel() {
            $("#modal-action-detail").on('loaded.bs.modal', function (e) {

			}).on('hidden.bs.modal', function (e) {
				$(this).removeData('bs.modal');
            });

            $("#modal-action-cart").on('loaded.bs.modal', function (e) {

            }).on('hidden.bs.modal', function (e) {
	            $(this).removeData('bs.modal');
            });
		}
		$this.init = function () {
			initilizeModel();
		}
	}

	$(function() {
		var self = new User();
		self.init();
	});
}(jQuery)) 