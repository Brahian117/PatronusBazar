import { Component } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css'],
})
export class CartComponent {
  cartItems: any[] = [
    {
      "id": 2,
      "title": "Narcissa Malfoy's Wand",
      "description": "Add this high-quality wand replica to your Harry Potter collection with this bespoke Narcissa Malfoy's wand recreation.Sculpted from resin and measuring approximately 34cm long, the unique wand is the perfect gift for any aspiring witch or wizard. The wand is available in a sleek, collectable wand box for you to display anywhere in your home and features the Warner Bros. Studio Tour London - The Making of Harry Potter logo.Wand measures approximately 13 3/4” in length. Please note this wand is a collectable replica, and not a toy.",
      "price": 48,
      "discountPercentage": 4,
      "rating": 4,
      "stock": 100,
      "brand": "Sample Brand",
      "category": "wands Category",
      "thumbnail": "https://harrypottershop.co.uk/cdn/shop/products/PLATFORM-NARCISSA-MALFOY-WAND_1_800x.png?v=1636369021",
      "images": [
        "https://harrypottershop.co.uk/cdn/shop/products/PLATFORM-NARCISSA-MALFOY-WAND_1_800x.png?v=1636369021"
      ]
    },
    // Add more items as needed
  ];

  getTotalPrice(): number {
    let totalPrice = 0;
    this.cartItems.forEach((item) => {
      totalPrice += item.price;
    });
    return totalPrice;
  }

  checkout() {
    // Implement the checkout logic here
    console.log('Proceeding to checkout...');
  }
}
