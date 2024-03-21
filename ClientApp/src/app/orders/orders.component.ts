import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orders: any[] = [  // Static array of orders
    { OrderId: 1, Date: '2023-12-27', UserId: 101, PaymentId: 201 },
    { OrderId: 2, Date: '2023-12-28', UserId: 102, PaymentId: 202 },
    { OrderId: 3, Date: '2023-12-29', UserId: 103, PaymentId: 203 },
    { OrderId: 4, Date: '2023-12-30', UserId: 104, PaymentId: 204 },
    { OrderId: 5, Date: '2023-12-31', UserId: 105, PaymentId: 205 },
    { OrderId: 6, Date: '2024-01-01', UserId: 106, PaymentId: 206 },
    { OrderId: 7, Date: '2024-01-02', UserId: 107, PaymentId: 207 },
    { OrderId: 8, Date: '2024-01-03', UserId: 108, PaymentId: 208 },
    { OrderId: 9, Date: '2024-01-04', UserId: 109, PaymentId: 209 },
    { OrderId: 10, Date: '2024-01-05', UserId: 110, PaymentId: 210 }
    // Add more orders as needed
  ];

  constructor() { }

  ngOnInit(): void {
  }
}
