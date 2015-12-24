function ChangeDropDown(newStatus, idDropdown) {
    if (newStatus == "Рассматривается") {
        $(idDropdown).find('option')
         .remove()
            .end()
            .append("<option>Рассматривается</option><option>Нелегальный</option><option>Легальный</option><option>Демонтирован</option>");
    }
    if (newStatus == "Нелегальный") {
        $(idDropdown).find('option')
         .remove()
            .end()
            .append("<option>Нелегальный</option><option>Легальный</option><option>Рассматривается</option><option>Демонтирован</option>");
    }
    if (newStatus == "Легальный") {
        $(idDropdown).find('option')
         .remove()
            .end()
            .append("<option>Легальный</option><option>Нелегальный</option><option>Рассматривается</option><option>Демонтирован</option>");
    }
    if (newStatus == "Демонтирован") {
        $(idDropdown).find('option')
         .remove()
            .end()
            .append("<option>Демонтирован</option><option>Легальный</option><option>Нелегальный</option><option>Рассматривается</option>");
    }
}