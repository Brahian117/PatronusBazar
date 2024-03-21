// app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { ShopComponent } from './shop/shop.component';
import { ProductsComponent } from './products/product.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { CarouselBannerComponent } from './crousal/carousel-banner.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { OrdersComponent } from './orders/orders.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegistrationComponent,
    LoginComponent,
    ShopComponent,
    ProductsComponent,
    AboutUsComponent,
    CarouselBannerComponent,
    ProductDetailComponent,
    OrdersComponent, // Don't forget to add ProductDetailComponent here
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent },
      { path: 'shop', component: ShopComponent },
      { path: 'product-detail/:id', component: ProductDetailComponent },
      { path: 'about-us', component: AboutUsComponent },
      { path: 'orders', component: OrdersComponent },
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
