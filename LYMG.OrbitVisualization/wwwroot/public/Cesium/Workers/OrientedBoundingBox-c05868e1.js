define(["exports","./when-54c2dc71","./Check-6c0211bc","./Math-fc8cecf5","./Cartesian2-a8ce88a9","./Transforms-9ac5213c","./Plane-29afec1f","./EllipsoidTangentPlane-97448adb"],function(a,q,t,X,j,T,Z,G){"use strict";function v(a,t){this.center=j.Cartesian3.clone(q.defaultValue(a,j.Cartesian3.ZERO)),this.halfAxes=T.Matrix3.clone(q.defaultValue(t,T.Matrix3.ZERO))}v.packedLength=j.Cartesian3.packedLength+T.Matrix3.packedLength,v.pack=function(a,t,e){return e=q.defaultValue(e,0),j.Cartesian3.pack(a.center,t,e),T.Matrix3.pack(a.halfAxes,t,e+j.Cartesian3.packedLength),t},v.unpack=function(a,t,e){return t=q.defaultValue(t,0),q.defined(e)||(e=new v),j.Cartesian3.unpack(a,t,e.center),T.Matrix3.unpack(a,t+j.Cartesian3.packedLength,e.halfAxes),e};var R=new j.Cartesian3,I=new j.Cartesian3,E=new j.Cartesian3,L=new j.Cartesian3,z=new j.Cartesian3,S=new j.Cartesian3,U=new T.Matrix3,V={unitary:new T.Matrix3,diagonal:new T.Matrix3};v.fromPoints=function(a,t){if(q.defined(t)||(t=new v),!q.defined(a)||0===a.length)return t.halfAxes=T.Matrix3.ZERO,t.center=j.Cartesian3.ZERO,t;for(var e=a.length,n=j.Cartesian3.clone(a[0],R),r=1;r<e;r++)j.Cartesian3.add(n,a[r],n);var i=1/e;j.Cartesian3.multiplyByScalar(n,i,n);var s,o=0,C=0,c=0,u=0,l=0,d=0;for(r=0;r<e;r++)o+=(s=j.Cartesian3.subtract(a[r],n,I)).x*s.x,C+=s.x*s.y,c+=s.x*s.z,u+=s.y*s.y,l+=s.y*s.z,d+=s.z*s.z;o*=i,C*=i,c*=i,u*=i,l*=i,d*=i;var h=U;h[0]=o,h[1]=C,h[2]=c,h[3]=C,h[4]=u,h[5]=l,h[6]=c,h[7]=l,h[8]=d;var x=T.Matrix3.computeEigenDecomposition(h,V),M=T.Matrix3.clone(x.unitary,t.halfAxes),m=T.Matrix3.getColumn(M,0,L),f=T.Matrix3.getColumn(M,1,z),p=T.Matrix3.getColumn(M,2,S),g=-Number.MAX_VALUE,w=-Number.MAX_VALUE,y=-Number.MAX_VALUE,O=Number.MAX_VALUE,b=Number.MAX_VALUE,P=Number.MAX_VALUE;for(r=0;r<e;r++)s=a[r],g=Math.max(j.Cartesian3.dot(m,s),g),w=Math.max(j.Cartesian3.dot(f,s),w),y=Math.max(j.Cartesian3.dot(p,s),y),O=Math.min(j.Cartesian3.dot(m,s),O),b=Math.min(j.Cartesian3.dot(f,s),b),P=Math.min(j.Cartesian3.dot(p,s),P);m=j.Cartesian3.multiplyByScalar(m,.5*(O+g),m),f=j.Cartesian3.multiplyByScalar(f,.5*(b+w),f),p=j.Cartesian3.multiplyByScalar(p,.5*(P+y),p);var N=j.Cartesian3.add(m,f,t.center);j.Cartesian3.add(N,p,N);var A=E;return A.x=g-O,A.y=w-b,A.z=y-P,j.Cartesian3.multiplyByScalar(A,.5,A),T.Matrix3.multiplyByScale(t.halfAxes,A,t.halfAxes),t};var M=new j.Cartesian3,m=new j.Cartesian3;function F(a,t,e,n,r,i,s,o,C,c,u){q.defined(u)||(u=new v);var l=u.halfAxes;T.Matrix3.setColumn(l,0,t,l),T.Matrix3.setColumn(l,1,e,l),T.Matrix3.setColumn(l,2,n,l),(x=M).x=(r+i)/2,x.y=(s+o)/2,x.z=(C+c)/2;var d=m;d.x=(i-r)/2,d.y=(o-s)/2,d.z=(c-C)/2;var h=u.center,x=T.Matrix3.multiplyByVector(l,x,x);return j.Cartesian3.add(a,x,h),T.Matrix3.multiplyByScale(l,d,l),u}var Y=new j.Cartographic,H=new j.Cartesian3,J=new j.Cartographic,K=new j.Cartographic,Q=new j.Cartographic,$=new j.Cartographic,aa=new j.Cartographic,ta=new j.Cartesian3,ea=new j.Cartesian3,na=new j.Cartesian3,ra=new j.Cartesian3,ia=new j.Cartesian3,sa=new j.Cartesian2,oa=new j.Cartesian2,Ca=new j.Cartesian2,ca=new j.Cartesian2,ua=new j.Cartesian2,la=new j.Cartesian3,da=new j.Cartesian3,ha=new j.Cartesian3,xa=new j.Cartesian3,Ma=new j.Cartesian2,ma=new j.Cartesian3,fa=new j.Cartesian3,pa=new j.Cartesian3,ga=new Z.Plane(j.Cartesian3.UNIT_X,0);v.fromRectangle=function(a,t,e,n,r){if(t=q.defaultValue(t,0),e=q.defaultValue(e,0),n=q.defaultValue(n,j.Ellipsoid.WGS84),a.width<=X.CesiumMath.PI){var i,s=j.Rectangle.center(a,Y),o=n.cartographicToCartesian(s,H),C=new G.EllipsoidTangentPlane(o,n),c=C.plane,u=s.longitude,l=a.south<0&&0<a.north?0:s.latitude,d=j.Cartographic.fromRadians(u,a.north,e,J),h=j.Cartographic.fromRadians(a.west,a.north,e,K),x=j.Cartographic.fromRadians(a.west,l,e,Q),M=j.Cartographic.fromRadians(a.west,a.south,e,$),m=j.Cartographic.fromRadians(u,a.south,e,aa),f=n.cartographicToCartesian(d,ta),p=n.cartographicToCartesian(h,ea),g=n.cartographicToCartesian(x,na),w=n.cartographicToCartesian(M,ra),y=n.cartographicToCartesian(m,ia),O=C.projectPointToNearestOnPlane(f,sa),b=C.projectPointToNearestOnPlane(p,oa),P=C.projectPointToNearestOnPlane(g,Ca),N=C.projectPointToNearestOnPlane(w,ca),A=C.projectPointToNearestOnPlane(y,ua),T=-(i=Math.min(b.x,P.x,N.x)),v=Math.max(b.y,O.y),R=Math.min(N.y,A.y);return h.height=M.height=t,p=n.cartographicToCartesian(h,ea),w=n.cartographicToCartesian(M,ra),k=Math.min(Z.Plane.getPointDistance(c,p),Z.Plane.getPointDistance(c,w)),W=e,F(C.origin,C.xAxis,C.yAxis,C.zAxis,i,T,R,v,k,W,r)}var I=0<a.south,E=a.north<0,L=I?a.south:E?a.north:0,z=j.Rectangle.center(a,Y).longitude,S=j.Cartesian3.fromRadians(z,L,e,n,la);S.z=0;var U=Math.abs(S.x)<X.CesiumMath.EPSILON10&&Math.abs(S.y)<X.CesiumMath.EPSILON10?j.Cartesian3.UNIT_X:j.Cartesian3.normalize(S,da),V=j.Cartesian3.UNIT_Z,B=j.Cartesian3.cross(U,V,ha);c=Z.Plane.fromPointNormal(S,U,ga);var _=j.Cartesian3.fromRadians(z+X.CesiumMath.PI_OVER_TWO,L,e,n,xa);i=-(T=j.Cartesian3.dot(Z.Plane.projectPointOntoPlane(c,_,Ma),B)),v=j.Cartesian3.fromRadians(0,a.north,E?t:e,n,ma).z,R=j.Cartesian3.fromRadians(0,a.south,I?t:e,n,fa).z;var k,W,D=j.Cartesian3.fromRadians(a.east,L,e,n,pa);return F(S,B,V,U,i,T,R,v,k=Z.Plane.getPointDistance(c,D),W=0,r)},v.clone=function(a,t){if(q.defined(a))return q.defined(t)?(j.Cartesian3.clone(a.center,t.center),T.Matrix3.clone(a.halfAxes,t.halfAxes),t):new v(a.center,a.halfAxes)},v.intersectPlane=function(a,t){var e=a.center,n=t.normal,r=a.halfAxes,i=n.x,s=n.y,o=n.z,C=Math.abs(i*r[T.Matrix3.COLUMN0ROW0]+s*r[T.Matrix3.COLUMN0ROW1]+o*r[T.Matrix3.COLUMN0ROW2])+Math.abs(i*r[T.Matrix3.COLUMN1ROW0]+s*r[T.Matrix3.COLUMN1ROW1]+o*r[T.Matrix3.COLUMN1ROW2])+Math.abs(i*r[T.Matrix3.COLUMN2ROW0]+s*r[T.Matrix3.COLUMN2ROW1]+o*r[T.Matrix3.COLUMN2ROW2]),c=j.Cartesian3.dot(n,e)+t.distance;return c<=-C?T.Intersect.OUTSIDE:C<=c?T.Intersect.INSIDE:T.Intersect.INTERSECTING};var x=new j.Cartesian3,f=new j.Cartesian3,p=new j.Cartesian3,h=new j.Cartesian3;v.distanceSquaredTo=function(a,t){var e=j.Cartesian3.subtract(t,a.center,M),n=a.halfAxes,r=T.Matrix3.getColumn(n,0,x),i=T.Matrix3.getColumn(n,1,f),s=T.Matrix3.getColumn(n,2,p),o=j.Cartesian3.magnitude(r),C=j.Cartesian3.magnitude(i),c=j.Cartesian3.magnitude(s);j.Cartesian3.normalize(r,r),j.Cartesian3.normalize(i,i),j.Cartesian3.normalize(s,s);var u=h;u.x=j.Cartesian3.dot(e,r),u.y=j.Cartesian3.dot(e,i),u.z=j.Cartesian3.dot(e,s);var l,d=0;return u.x<-o?d+=(l=u.x+o)*l:u.x>o&&(d+=(l=u.x-o)*l),u.y<-C?d+=(l=u.y+C)*l:u.y>C&&(d+=(l=u.y-C)*l),u.z<-c?d+=(l=u.z+c)*l:u.z>c&&(d+=(l=u.z-c)*l),d};var g=new j.Cartesian3,w=new j.Cartesian3;v.computePlaneDistances=function(a,t,e,n){q.defined(n)||(n=new T.Interval);var r=Number.POSITIVE_INFINITY,i=Number.NEGATIVE_INFINITY,s=a.center,o=a.halfAxes,C=T.Matrix3.getColumn(o,0,x),c=T.Matrix3.getColumn(o,1,f),u=T.Matrix3.getColumn(o,2,p),l=j.Cartesian3.add(C,c,g);j.Cartesian3.add(l,u,l),j.Cartesian3.add(l,s,l);var d=j.Cartesian3.subtract(l,t,w),h=j.Cartesian3.dot(e,d),r=Math.min(h,r),i=Math.max(h,i);return j.Cartesian3.add(s,C,l),j.Cartesian3.add(l,c,l),j.Cartesian3.subtract(l,u,l),j.Cartesian3.subtract(l,t,d),h=j.Cartesian3.dot(e,d),r=Math.min(h,r),i=Math.max(h,i),j.Cartesian3.add(s,C,l),j.Cartesian3.subtract(l,c,l),j.Cartesian3.add(l,u,l),j.Cartesian3.subtract(l,t,d),h=j.Cartesian3.dot(e,d),r=Math.min(h,r),i=Math.max(h,i),j.Cartesian3.add(s,C,l),j.Cartesian3.subtract(l,c,l),j.Cartesian3.subtract(l,u,l),j.Cartesian3.subtract(l,t,d),h=j.Cartesian3.dot(e,d),r=Math.min(h,r),i=Math.max(h,i),j.Cartesian3.subtract(s,C,l),j.Cartesian3.add(l,c,l),j.Cartesian3.add(l,u,l),j.Cartesian3.subtract(l,t,d),h=j.Cartesian3.dot(e,d),r=Math.min(h,r),i=Math.max(h,i),j.Cartesian3.subtract(s,C,l),j.Cartesian3.add(l,c,l),j.Cartesian3.subtract(l,u,l),j.Cartesian3.subtract(l,t,d),h=j.Cartesian3.dot(e,d),r=Math.min(h,r),i=Math.max(h,i),j.Cartesian3.subtract(s,C,l),j.Cartesian3.subtract(l,c,l),j.Cartesian3.add(l,u,l),j.Cartesian3.subtract(l,t,d),h=j.Cartesian3.dot(e,d),r=Math.min(h,r),i=Math.max(h,i),j.Cartesian3.subtract(s,C,l),j.Cartesian3.subtract(l,c,l),j.Cartesian3.subtract(l,u,l),j.Cartesian3.subtract(l,t,d),h=j.Cartesian3.dot(e,d),r=Math.min(h,r),i=Math.max(h,i),n.start=r,n.stop=i,n};var n=new T.BoundingSphere;v.isOccluded=function(a,t){var e=T.BoundingSphere.fromOrientedBoundingBox(a,n);return!t.isBoundingSphereVisible(e)},v.prototype.intersectPlane=function(a){return v.intersectPlane(this,a)},v.prototype.distanceSquaredTo=function(a){return v.distanceSquaredTo(this,a)},v.prototype.computePlaneDistances=function(a,t,e){return v.computePlaneDistances(this,a,t,e)},v.prototype.isOccluded=function(a){return v.isOccluded(this,a)},v.equals=function(a,t){return a===t||q.defined(a)&&q.defined(t)&&j.Cartesian3.equals(a.center,t.center)&&T.Matrix3.equals(a.halfAxes,t.halfAxes)},v.prototype.clone=function(a){return v.clone(this,a)},v.prototype.equals=function(a){return v.equals(this,a)},a.OrientedBoundingBox=v});
