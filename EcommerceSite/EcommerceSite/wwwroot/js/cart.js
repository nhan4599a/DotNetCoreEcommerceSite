$('.add-to-cart > .add-to-cart-btn').click(function () {
    var productId = $(this).parent().parent().attr('id');
    addToCart(productId);
});

function addToCart(productId) {
    $.post('/Cart/Add', $.param({ productId: productId }), function (data) {
        if (data.responseCode === 200) {
            toastr.success(data.data, 'Success');
            var currVal = parseInt($('#cartQty').html());
            $('#cartQty').html(currVal + 1);
        } else {
            toastr.error(data.errorMessage, 'Error sdasdad');
        }
    });
}

function updateCartClicked() {
    var objArr = [];
    $('.cart-list').children('.product-widget').each(function () {
        var id = $(this).children('p').html();
        var quantity = $(this).children('.product-body').children('.product-price').children('.qty').val();
        objArr.push({ productId: id, quantity: quantity });
    });
    $('#btnCart').removeClass('open');
    $.post('/Cart/Update', { list: objArr }, function (data) {
        console.log(data);
        toastr.options.closeButton = true;
        if (data.responseCode === 200)
            toastr.success(data.data, 'Success');
        else
            toastr.error(data.errorMessage, 'Error');
        $('#btnCart').addClass('open');
    });
}

function deleteCartItem() {
    $('#btnCart').removeClass('open');
    var id = $(this).parent().children('p').html();
    $.post('/Cart/Delete', $.param({ productId: id }), function (data) {
        if (data.responseCode === 200) {
            var currVal = parseInt($('#cartQty').html());
            $('#cartQty').html(currVal - 1);
            toastr.success(data.data, 'Success');
        }
        else
            toastr.error(data.errorMessage, "Error");
        $('#btnCart').addClass('open');
    });
}

function loadCart() {
    var animation = lottie.loadAnimation({
        container: document.querySelector('div.animation-container'),
        renderer: 'svg',
        loop: true,
        autoplay: true,
        path: '/loading.json'
    });
    $.get('/Cart/Index', function (data) {
        animation.destroy();
        var obj = JSON.parse(data);
        var str = '';
        var count = 0;
        var price = 0;
        for (var item of obj) {
            str += `<div class="product-widget">
                        <p style="display: none">${item.ProductId}</p>
                        <div class="product-img">
                            <img src="${item.ImageUrl}" alt="error!" />
                        </div>
                        <div class="product-body">
                            <h3 class="product-name"><a href="#">${item.ProductName}</a></h3>
                            <h4 class="product-price"><input class="qty" style="width: 40%" type="number" value="${item.Quantity}" min="1" max="10"/> x $${item.Price}</h4>
                                    </div>
                                    <button class="delete"><i class="fa fa-close"></i></button>
                        </div>`;
            count += item.Quantity;
            price += item.Quantity * item.Price;
        }
        if (str.length === 0)
            str = 'You don\'t have any items in your cart!';
        var entry = `<div class="animation-container"></div>
                                <div class="cart-list">${str}</div>
                                <div class="cart-summary">
                                    <small>${count} Item(s) in cart</small>
                                    <h5>SUBTOTAL: $${price}</h5>
                                </div>
                                <div class="cart-btns">
                                    <a href="#" id="btnUpdate">Update Cart</a>
                                    <a href="@Url.Action("Create", "Bill")">Checkout
                                        <i class="fa fa-arrow-circle-right"></i>
                                    </a>
                                </div>`;
        $('.cart-dropdown').html(entry);

        $('#btnUpdate').click(updateCartClicked);
        $('button.delete').click(deleteCartItem);
    });
}

$('#btnCart').on('classChanged', function () {
    if ($(this).hasClass('open'))
        loadCart();
});