language core;

extend with #references, #type-cast, #pairs, #top-type, #structural-subtyping;

fn dup(x : Top) -> { Top, Top } {
	return { x, x }
}

fn main(n : Nat) -> &Top {
	return (dup(n) cast as {Nat, Nat}).1
}