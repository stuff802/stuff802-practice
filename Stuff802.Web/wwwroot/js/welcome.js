$(function () {

    const key = "wmdc_designcodewelcome";
    var dont = localStorage.getItem(key);
    var pageId = $('meta[name=pageId]').attr("content");

    if (dont == null || dont == "false") {
        // Show the welcome model
        $.get("/umbraco/api/getpageproperty", { "pageId": pageId, "pageProperty": "welcomeMessage"}, (message) => {
            try {
                message = message.trim().replace(/(?:\r\n|\r|\n)/g, '<br>');
            }
            catch {
                message = "";
            }

            if (message.length > 0) {
                var $injectTo = $("body").first();
                var title = "Wakefield District Design Code";

                var $modal = $(`<div class="modal" tabindex="-1">
                    <div class="modal-dialog modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">${title}</h5>
                            </div>
                            <div class="modal-body">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <p>${message}</p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 mt-3">
                                            <input type="checkbox" id="chkWelcome" name="chkWelcome" value="1">
                                            <label for="chkWelcome"> Don&rsquo;t show this again</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="modal-btn-ok" class="btn btn-success" data-bs-dismiss="modal">OK</button>
                            </div>
                        </div>
                    </div>
                </div>`);

                var $chkWelcome = $modal.find("input[type=checkbox]:first");

                // Destroy itself on close
                $modal.on('hide.bs.modal', function (e) {
                    let chkd = $chkWelcome.is(":checked") ?? false;
                    localStorage.setItem(key, chkd);
                    $(this).off('hidden').remove();
                });

                $modal.appendTo($injectTo).modal('show');
            }
        });
    }
});