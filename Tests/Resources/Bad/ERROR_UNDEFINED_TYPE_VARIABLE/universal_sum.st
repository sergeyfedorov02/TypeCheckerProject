language core;

extend with #universal-types, #sum-types, #type-cast;

generic fn do[T](x : C + Nat) -> T {
	return match(x) {
    	inl(a) => a
    | inr(b) => 0 cast as T
   }
}

fn main(x : Nat) -> Nat {
	return do[Nat](inl(x))
}