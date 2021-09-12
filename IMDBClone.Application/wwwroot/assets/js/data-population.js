function AddData(data){
    var ihtml = ``;
    for(var i=0; i< data.length; i++){
        ihtml += `<section class="section section-lg section-safe section-style">
        <img src="/assets/img/path5.png" class="path">
        <div class="container">
            <div class="row row-grid justify-content-between">
                <div class="col-md-5">
                    <img src="/img/${data[i].fileName}" class="">
                </div>
                <div class="col-md-6">
                    <div class="px-md-5">
                        <hr class="line-success">
                        <h3><img src="/img/${data[i].fileName}" style="width: 30px; height: 30px; border-radius:10px" class="mr-2" alt="Alternate Text" />${data[i].title}</h3>
                        <p>${data[i].description}</p>
                        <ul class="list-unstyled mt-5">
                            <li class="py-2">
                                <div class="d-flex align-items-center">
                                    <div class="icon icon-success mb-2">
                                        <i class="tim-icons icon-vector"></i>
                                    </div>
                                    <div class="ml-3">
                                        <h6>Release date: ${data[i].releaseDate.substring(0,10)}</h6>
                                    </div>
                                </div>
                            </li>
                            <li class="py-2">
                                <div class="d-flex align-items-center">
                                    <div class="icon icon-success mb-2">
                                        <i class="tim-icons icon-tap-02"></i>
                                    </div>
                                    <div class="ml-3">
                                        <h6>${data[i].movieType == 1 ? "Movie" : "TV Show"}</h6>
                                    </div>
                                </div>
                            </li>
                            <li class="py-2">
                                <div class="d-flex align-items-center">
                                    <div class="icon icon-success mb-2">
                                        <i class="tim-icons icon-single-02"></i>
                                    </div>
                                    <div class="ml-3">
                                        <h6>${ConvertActorsToString(data[i].actors)}</h6>
                                    </div>
                                </div>
                            </li>
                            <li class="py-2">
                                <div class="rating-box">
                                    <div class="d-flex align-items-center">
                                        <div class="icon icon-success mb-2">
                                            <i class="tim-icons icon-tap-02"></i>
                                        </div>
                                        <div class="ml-3">
                                            <h6>RATING: ${data[i].averageRating} (average)</h6>
                                        </div>
                                    </div>
                                    <div class="rating-container">
                                    <input type="radio" name="rating" data-id="${data[i].movieId}" value="5" id="star-5" onchange="changeEvent(event);" ${parseInt(data[i].averageRating) == 5 ? "checked": ""}> <label for="star-5">&#9733;</label>
                                    
                                    <input type="radio" name="rating" data-id="${data[i].movieId}" value="4" id="star-4" onchange="changeEvent(event);" ${parseInt(data[i].averageRating) == 4 ? "checked": ""}> <label for="star-4">&#9733;</label>
                                    
                                    <input type="radio" name="rating" data-id="${data[i].movieId}" value="3" id="star-3" onchange="changeEvent(event);" ${parseInt(data[i].averageRating) == 3 ? "checked": ""}> <label for="star-3">&#9733;</label>
                                    
                                    <input type="radio" name="rating" data-id="${data[i].movieId}" value="2" id="star-2" onchange="changeEvent(event);" ${parseInt(data[i].averageRating) == 2 ? "checked": ""}> <label for="star-2">&#9733;</label>
                                    
                                    <input type="radio" name="rating" data-id="${data[i].movieId}" value="1" id="star-1" onchange="changeEvent(event);" ${parseInt(data[i].averageRating) == 1 ? "checked": ""}> <label for="star-1">&#9733;</label>
                                </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

    </section>`
    }

    $(`#body-movies`).empty().append(ihtml);
}

function ConvertActorsToString(actors){
    var text = "";
    for(var i =0; i< actors.length; i++){
        text += actors[i].fullName + ",";
    }
    return text.slice(0, -1);
}


function changeEvent(event){
    var movie = $(event.target).attr("data-id");
    console.log(movie);
    var starRating = $(event.target).val();
    var jObject = {
        "Rate": starRating,
        "MovieId": movie
    }
    var token = sessionStorage.getItem(`IMDB_A_C_E_Saved`)
        if(token == null || token == "" || token == undefined)
            location.href="/index.html";
        $.ajax({
            url: 'https://localhost:5001/api/rating',
            type: 'POST',
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', `Bearer ${token}`);
            },
            headers: {
                "content-type": "application/json;charset=UTF-8" // Add this line
            },
            data: JSON.stringify(jObject),
            success: function (data) {
                console.log(`success ${data}`)
            },
            error: function (data) {console.log(`ERROR: ${JSON.stringify(data)}`) },
        });

}