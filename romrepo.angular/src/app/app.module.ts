import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopnavComponent } from './component/topnav/topnav.component';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { SystemSettingsComponent } from './component/system-settings/system-settings.component';

@NgModule({
  declarations: [
    AppComponent,
    TopnavComponent,
    SystemSettingsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    provideHttpClient(
      withFetch(), 
      withInterceptors([]),
    )
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
