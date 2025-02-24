$(function () {
    $("#head-checkbox").on("change", function () {
        let isChecked = $(this).prop("checked");
        $(".row-checkbox").prop("checked", isChecked);
        let isNoChecked = $('.row-checkbox:checked').length === 0;
        $("#btn-submit-ids").prop("disabled", isNoChecked);
    })
    $(".row-checkbox").on("change", function () {
        let isAnyUnchecked = $('.row-checkbox:not(:checked)').length > 0;
        $("#head-checkbox").prop("checked", !isAnyUnchecked);
        let isNoChecked = $('.row-checkbox:checked').length === 0;
        $("#btn-submit-ids").prop("disabled", isNoChecked);
    })

    $("#btn-submit-ids").on("click", function () {
        let selectedIds = [];
        $(".row-checkbox").each(function () {
            if ($(this).is(":checked")) {
                selectedIds.push($(this).val());
            }
        })
        let selectedIdsString = selectedIds.join(",");
        $("#selected-ids").val(selectedIdsString);
        $("#form-selected-ids").submit();
    })

})

