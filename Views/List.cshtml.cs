@model IEnumerable<News>

@{
    ViewData["Title"] = "Haber Listesi";
Layout = "~/Views/Shared/AdminLayout.cshtml";
}

< div class= "main-panel" >
    < div class= "content-wrapper" >
        < div class= "page-header" >
            < h3 class= "page-title" > Haber Listesi </ h3 >
        </ div >

        < div class= "row" >
            < div class= "col-lg-12 grid-margin stretch-card" >
                < div class= "card" >
                    < div class= "card-body" >
                        < div class= "table-responsive" >
                            < table class= "table table-striped" >
                                < thead >
                                    < tr >
                                        < th > Baþlýk </ th >
                                        < th > Açýklama </ th >
                                        < th > Görsel </ th >
                                        < th > Tarih </ th >
                                        < th > Ýþlemler </ th >
                                    </ tr >
                                </ thead >
                                < tbody >
                                    @foreach(var news in Model)
                                    {
                                        < tr >
                                            < td > @news.Title </ td >
                                            < td > @news.Description </ td >
                                            < td >< img src = "@news.ImagePath" style = "width:100px" /></ td >
                                            < td > @news.Date.ToShortDateString() </ td >
                                            < td >
                                                < a href = "/News/Edit/@news.Id" class= "btn btn-warning btn-sm" > Güncelle </ a >
                                                < form asp - action = "Delete" asp - route - id = "@news.Id" method = "post" style = "display:inline;" >
                                                    < button type = "submit" class= "btn btn-danger btn-sm"
                                                            onclick = "return confirm('Silmek istediðine emin misin?')" >
                                                        Sil
                                                    </ button >
                                                </ form >
                                            </ td >
                                        </ tr >
                                    }
                                </ tbody >
                            </ table >
                        </ div >
                        < a href = "/News/Create" class= "btn btn-gradient-primary mt-3" > Yeni Haber Ekle</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
