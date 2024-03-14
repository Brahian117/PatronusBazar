import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
})
export class ProductDetailComponent implements OnInit {
  productId: any;
  product: any;
  errorMessage: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.productId = params['id']; 
      if (this.productId) {
        this.getProductDetails();
      } else {
        this.errorMessage = 'Product ID is not provided';
        console.error(this.errorMessage);
      }
    });
  }
  

  getProductDetails() {
    this.http.get(this.baseUrl + `product/${this.productId}`)
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
    // Implement your logic to add the product to the cart
    console.log('Product added to cart');
  }
}
