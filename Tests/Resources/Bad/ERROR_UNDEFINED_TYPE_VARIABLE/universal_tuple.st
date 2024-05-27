language core;

extend with #universal-types, #tuples;

generic fn do[T](x : {C, Bool}) -> T {
	return x.1
}

fn main(x : Nat) -> Nat {
	return do[Nat]({x, true})
}