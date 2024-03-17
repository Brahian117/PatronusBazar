import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
})
export class ProductDetailComponent implements OnInit {
  id: any;
  product: any;
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private cartService: CartService
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.getProductDetails(this.id);
  }

  getProductDetails(id: any) {
    this.http.get(`http://localhost:4000/products/${id}`)
      .subscribe(
        (data) => {
          this.product = data;
          console.log(this.product);
        },
        (error) => {
          if (error.status === 404) {
            this.errorMessage = 'Product not found';
          } else {
            this.errorMessage = 'Error fetching product details';
          }
          console.error(this.errorMessage, error);
        }
      );
  }

  addToCart() {
    const cartItem = {
      ProductId: this.product.ProductId,
      UserId: 1, 
      Quantity: 1 
    };
    
    this.http.post('http://localhost:4000/addproduct', cartItem)
      .subscribe(
        () => {
          this.cartService.addToCart(this.product);
        },
        (error) => {
          console.error('Error adding product to cart:', error);
        }
      );
  }
}
