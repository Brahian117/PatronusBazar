import { Component } from '@angular/core';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
//  styleUrls: ['./shop.component.css']
})
export class ShopComponent {
  items: any[] = [
    { name: 'Harry Potter and the Sorcerer\'s Stone', price: 10 },
    { name: 'Harry Potter and the Chamber of Secrets', price: 12 },
    { name: 'Harry Potter and the Prisoner of Azkaban', price: 14 },
    // Add more items as needed
  ];

  constructor() { }

  // Add other methods as needed
}
