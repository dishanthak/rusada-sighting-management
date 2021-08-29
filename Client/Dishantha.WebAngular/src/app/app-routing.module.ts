import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ListSightingComponent } from './sightings';

const routes: Routes = [{ path: '', component: ListSightingComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
